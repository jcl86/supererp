using SuperErp.Core;
using System.Globalization;
using System.Security.Claims;

namespace SuperErp.Management.Domain
{
    public static class ClaimConverters
    {
        public static Claim ToClaim(this string value, string claimType) => new Claim(claimType, value);
        public static Claim ToClaim(this int value, string claimType) => new Claim(claimType, value.ToString());
        public static Claim ToClaim(this DateTime value, string claimType) => new Claim(claimType, value.ToString(CultureInfo.InvariantCulture));
        public static Claim ToClaim(this decimal value, string claimType) => new Claim(claimType, value.ToString(CultureInfo.InvariantCulture));
        public static Claim ToClaim<T>(this T value, string claimType) where T : Enum => new Claim(claimType, value.ToString());
        public static Claim ToClaim(this bool value, string claimType) => new Claim(claimType, value.ToString());
    }
}
