using TimeTracker.Core.Auth.Interfaces;
using TimeTracker.Core.Auth.Models.Dto;
using TimeTracker.Core.Auth.UseCases.ChangePassword;
using TimeTracker.Core.Auth.UseCases.CreateAccount;
using TimeTracker.Core.Auth.UseCases.GetUserProfile;
using TimeTracker.Core.Auth.UseCases.ResetPassword;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Server.AspNetCore;
using TimeTracker.Core.Shared.Models.Dto;

namespace TimeTracker.Web.Controllers;

public class AccountsController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly ITokenNotificationBroker _notificationBroker;
    private const string ResetUrl = "authentication/reset-password";

    public AccountsController(IMediator mediator, ITokenNotificationBroker notificationBroker)
    {
        _mediator = mediator;
        _notificationBroker = notificationBroker;
    }

    [HttpPost]
    public async Task<ActionResult<Envelope>> CreateAccount([FromBody] CreateAccountCommand request)
    {
        var registerUser =
            await _mediator.Send(request);
        if (registerUser.IsSuccess)
        {
            return Ok(Envelope.Ok());
        }

        return UnprocessableEntity(Envelope.Error(registerUser.Error));
    }

    [HttpPost("forgot-password")]
    public async Task<ActionResult<Envelope>> ForgotPassword([FromBody] InitiatePasswordResetCommand request)
    {
        var initiateResetPassword = await _mediator.Send(request);
        if (initiateResetPassword.IsFailure)
        {
            return BadRequest(Envelope.Error(initiateResetPassword.Error));
        }

        var sendToken = await _notificationBroker.SendToken(request.Email,
            initiateResetPassword.Value.Name, "Your password reset link", initiateResetPassword.Value.Token,
            ResetUrl);

        if (sendToken.IsFailure)
        {
            return UnprocessableEntity(Envelope.Error(sendToken.Error));
        }

        return Ok(Envelope.Ok("A reset link was sent!"));
    }

    [HttpPost("reset-password")]
    public async Task<ActionResult<Envelope>> ResetPassword([FromBody] ResetPasswordCommand request)
    {
        var initiateResetPassword = await _mediator.Send(request);
        if (initiateResetPassword.IsFailure)
        {
            return BadRequest(Envelope.Error(initiateResetPassword.Error));
        }

        return Ok(Envelope.Ok("Password has been reset successfully!"));
    }

    [Authorize(AuthenticationSchemes = OpenIddictServerAspNetCoreDefaults.AuthenticationScheme),
     HttpGet("~/connect/userinfo")]
    public async Task<ActionResult<Envelope<UserDto>>> GetProfile()
    {
        var getUserProfile = await _mediator.Send(new GetUserProfileQuery(AuthorizedUser));
        if (getUserProfile.IsSuccess)
        {
            return Ok(Envelope.Ok(getUserProfile.Value));
        }

        return BadRequest(Envelope.Error(getUserProfile.Error));
    }

    [HttpPost("change-password"), Authorize(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)]
    public async Task<ActionResult<Envelope>> ChangePassword([FromBody] ChangePasswordCommand request)
    {
        var changePassword =
            await _mediator.Send(request);

        if (changePassword.IsSuccess)
        {
            await HttpContext.SignOutAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            return Ok(Envelope.Ok("Please sign in again"));
        }

        return UnprocessableEntity(Envelope.Error(changePassword.Error));
    }
}