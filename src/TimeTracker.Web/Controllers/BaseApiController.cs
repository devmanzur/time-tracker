using System.Security.Claims;
using TimeTracker.Core.Auth.Models.Dto;
using TimeTracker.Core.Auth.Utils;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;

namespace TimeTracker.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        private const string PreferredUsername = "preferred_username";
        private const string UserId = "sub";

        protected AuthorizedUser AuthorizedUser => new AuthorizedUser(User.GetClaimValue(OpenIddictConstants.Claims.Subject)!,
            User.GetClaimValue(OpenIddictConstants.Claims.Email)!);
        
    }
}