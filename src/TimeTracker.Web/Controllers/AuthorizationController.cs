using System.Security.Claims;
using TimeTracker.Core.Auth.Models.Entities;
using TimeTracker.Core.Auth.UseCases.AuthenticateUser;
using TimeTracker.Core.Auth.Utils;
using TimeTracker.Core.Shared.Utils;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

namespace TimeTracker.Web.Controllers;

public class AuthorizationController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IOptions<IdentityOptions> _identityOptions;

    public AuthorizationController(IMediator mediator, UserManager<ApplicationUser> userManager,
        IOptions<IdentityOptions> identityOptions)
    {
        _mediator = mediator;
        _userManager = userManager;
        _identityOptions = identityOptions;
    }

    [HttpGet("~/connect/authorize")]
    [HttpPost("~/connect/authorize")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Authorize()
    {
        var request = HttpContext.GetOpenIddictServerRequest();

        var applicationAuthentication = await HttpContext.AuthenticateAsync(IdentityConstants.ApplicationScheme);

        if (applicationAuthentication.Succeeded)
        {
            return HandleSelfAuthorization(request);
        }

        if (request?.Username != null && request?.Password != null)
        {
            return await HandleResourceOwnerAuthorization(request);
        }


        return Challenge(
            authenticationSchemes: IdentityConstants.ApplicationScheme,
            properties: new AuthenticationProperties
            {
                RedirectUri = Request.PathBase + Request.Path + QueryString.Create(
                    Request.HasFormContentType ? Request.Form.ToList() : Request.Query.ToList())
            });
    }

    /// <summary>
    /// exchange credential/ auth token/ refresh token for access token
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    [AllowAnonymous]
    [HttpPost("~/connect/token")]
    public async Task<IActionResult> Exchange()
    {
        var openIdConnectRequest = HttpContext.GetOpenIddictServerRequest();

        if (openIdConnectRequest.HasValue())
        {
            if (openIdConnectRequest!.IsPasswordGrantType())
            {
                return await HandleResourceOwnerPasswordFlow(openIdConnectRequest!);
            }

            if (openIdConnectRequest!.IsRefreshTokenGrantType() ||
                openIdConnectRequest!.IsAuthorizationCodeFlow())
            {
                return await HandleAuthorizationFlow(openIdConnectRequest!);
            }

            if (openIdConnectRequest!.IsAuthorizationCodeGrantType())
            {
                return await HandleAuthorizationCodeGrant(openIdConnectRequest!);
            }

            if (openIdConnectRequest!.IsClientCredentialsGrantType())
            {
                return HandleClientCredentialFlow(openIdConnectRequest);
            }
        }

        throw new InvalidOperationException("The specified grant type is not supported.");
    }

    private IActionResult HandleClientCredentialFlow(OpenIddictRequest? request)
    {
        var identity = new ClaimsIdentity(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

        identity.AddClaim(OpenIddictConstants.Claims.Subject,
            request.ClientId ?? throw new InvalidOperationException());

        var claimsPrincipal = new ClaimsPrincipal(identity);

        foreach (var scope in request.GetScopes())
        {
            var resources = LocalIdentityConfig.GetResources(scope);
            if (resources.HasValue() && resources!.Any())
            {
                claimsPrincipal.SetResources(resources!);
            }
        }

        claimsPrincipal.SetScopes(request.GetScopes());
        return SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }


    //Handle authorization code flow
    private async Task<IActionResult> HandleAuthorizationCodeGrant(OpenIddictRequest dto)
    {
        var claimsPrincipal =
            (await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme))
            .Principal;
        Maybe<ApplicationUser> user =
            await _userManager.FindByEmailAsync(claimsPrincipal.GetClaimValue(OpenIddictConstants.Scopes.Email));
        if (user.HasValue)
        {
            var ticket = await IssueTicket(dto, user.Value, claimsPrincipal);
            return SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
        }

        return Forbid();
    }

    /// <summary>
    /// Returns new access token using the refresh token
    /// Here we can add extra checks such as, is the user still allowed to sign in
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    private async Task<IActionResult> HandleAuthorizationFlow(OpenIddictRequest dto)
    {
        var claimsPrincipal =
            (await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme))
            .Principal;
        return SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }

    /// <summary>
    /// Authenticate the user using provided username & password
    /// On successful authentication issue token
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    private async Task<IActionResult> HandleResourceOwnerPasswordFlow(OpenIddictRequest dto)
    {
        var authenticate =
            await _mediator.Send(new AuthenticateUserByPasswordCommand(dto.Username, dto.Password));
        if (authenticate.IsSuccess)
        {
            var ticket = await IssueTicket(dto, authenticate.Value.User, authenticate.Value.Principal);
            return SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
        }

        return Forbid(
            authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
            properties: new AuthenticationProperties(new Dictionary<string, string>
            {
                [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.AccessDenied,
                [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "Invalid username/ password"
            }!));
    }

    private AuthenticationTicket CreateTicketAsync(
        OpenIddictRequest request, ClaimsPrincipal principal,
        AuthenticationProperties? properties = null)
    {
        var ticket = new AuthenticationTicket(principal, properties,
            OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

        if (!request.IsRefreshTokenGrantType())
        {
            // Set the list of scopes granted to the client application.
            // Note: the offline_access scope must be granted
            // to allow OpenIddict to return a refresh token.
            ticket.Principal.SetScopes(new[]
            {
                OpenIddictConstants.Scopes.OpenId,
                OpenIddictConstants.Scopes.Email,
                OpenIddictConstants.Scopes.Profile,
                OpenIddictConstants.Scopes.OfflineAccess,
                OpenIddictConstants.Scopes.Roles
            }.Intersect(request.GetScopes()));
        }

        foreach (var claim in ticket.Principal.Claims)
        {
            // Never include the security stamp in the access and identity tokens, as it's a secret value.
            if (claim.Type == _identityOptions.Value.ClaimsIdentity.SecurityStampClaimType)
            {
                continue;
            }

            var destinations = new List<string>
            {
                OpenIddictConstants.Destinations.AccessToken
            };

            // Only add the iterated claim to the id_token if the corresponding scope was granted to the client application.
            // The other claims will only be added to the access_token, which is encrypted when using the default format.
            if (claim.Type == OpenIddictConstants.Claims.Name &&
                ticket.Principal.HasScope(OpenIddictConstants.Scopes.Profile) ||
                claim.Type == OpenIddictConstants.Claims.Email &&
                ticket.Principal.HasScope(OpenIddictConstants.Scopes.Email) ||
                claim.Type == OpenIddictConstants.Claims.Role &&
                ticket.Principal.HasScope(OpenIddictConstants.Scopes.Profile) ||
                claim.Type == OpenIddictConstants.Claims.PhoneNumber &&
                ticket.Principal.HasScope(OpenIddictConstants.Scopes.Profile)
               )
            {
                destinations.Add(OpenIddictConstants.Destinations.IdentityToken);
            }

            claim.SetDestinations(destinations);
        }

        return ticket;
    }

    private ClaimsPrincipal GetPrincipalWithUserClaims(ClaimsPrincipal principal, List<string> roles,
        ApplicationUser user)
    {
        var identity = principal.Identity as ClaimsIdentity;
        if (identity == null)
        {
            throw new Exception("invalid identity");
        }

        if (roles.Any())
        {
            var roleClaim = identity.Claims.Where(c => c.Type == "role").ToList();
            if (roleClaim.Any())
            {
                foreach (var claim in roleClaim)
                {
                    identity.RemoveClaim(claim);
                }
            }

            identity.AddClaims(roles.Select(r => new Claim(ClaimTypes.Role, r)));
        }

        //TODO: Add custom claims here
        identity.AddClaims(roles.Select(r => new Claim(CustomClaimTypes.IndividualId, user.UserName)));

        return new ClaimsPrincipal(identity);
    }

    private async Task<AuthenticationTicket> IssueTicket(OpenIddictRequest request, ApplicationUser user,
        ClaimsPrincipal principal)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var ticket = CreateTicketAsync(request, GetPrincipalWithUserClaims(principal, roles.ToList(), user));
        return ticket;
    }

    private IActionResult HandleSelfAuthorization(OpenIddictRequest request)
    {
        var claims = User.Claims;

        var claimsIdentity = new ClaimsIdentity(claims, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        claimsPrincipal.SetScopes(request.GetScopes());

        return SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }

    private async Task<IActionResult> HandleResourceOwnerAuthorization(OpenIddictRequest request)
    {
        var authenticate =
            await _mediator.Send(new AuthenticateUserByPasswordCommand(request.Username, request.Password));
        if (authenticate.IsSuccess)
        {
            var ticket = await IssueTicket(request, authenticate.Value.User, authenticate.Value.Principal);

            var claims = ticket.Principal.Claims;

            var claimsIdentity = new ClaimsIdentity(claims, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            claimsPrincipal.SetScopes(request.GetScopes());

            return SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        return Forbid(
            authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
            properties: new AuthenticationProperties(new Dictionary<string, string>
            {
                [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.AccessDenied,
                [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "Invalid username/ password"
            }!));
    }
}