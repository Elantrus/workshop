using System.Security.Claims;

namespace Workshop.API.Extensions;

public static class IdentityExtensions
{
    public static long GetUserId(this IEnumerable<Claim> claims)
    {
        return Convert.ToInt64(claims?.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);
    }
}