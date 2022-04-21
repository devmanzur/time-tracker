using System.Security.Claims;

namespace TimeTracker.Core.Auth.Utils
{
    public static class ClaimsExtensions
    {
        public static string? GetClaimValue(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            return claimsPrincipal.Claims.FirstOrDefault(x => x.Type == claimType)?.Value;
        }
    }
}