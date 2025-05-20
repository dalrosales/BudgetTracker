using System.Security.Claims;

namespace BudgetTracker.Helpers
{
    public static class ClaimsPrincipalExtensions
    {
        public static string? GetJwtToken(this ClaimsPrincipal user)
        {
            return user?.Claims.FirstOrDefault(c => c.Type == "Jwt")?.Value;
        }
    }
}