using System.Security.Claims;

namespace SuperErp.Core
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.Email);
        }

        public static string GetUserId(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public static string GetUserName(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.Name);
        }

        public static bool Is(this ClaimsPrincipal principal, string userId)
        {
            var currentUserId = GetUserId(principal);

            return string.Equals(currentUserId, userId, StringComparison.OrdinalIgnoreCase);
        }
    }
}