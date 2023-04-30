using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;
using static SuperErp.Management.Model.Endpoints;
using SuperErp.Management.Model;
using System.Globalization;

namespace SuperErp.Management.Domain
{
    public class User : IdentityUser
    {
        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

        public IEnumerable<string> RoleNames => UserRoles?.Select(x => x.Role?.Name)?.ToList() ?? new List<string>();

        public T? GetClaim<T>(string claimType) where T : struct
        {
            var claim = GetClaim(claimType);

            if (claim is null)
            {
                return null;
            }

            return (T)Convert.ChangeType(claim, typeof(T), CultureInfo.InvariantCulture);
        }

        public string GetClaim(string claimType)
        {
            var claim = Claims?.FirstOrDefault(x => x.ClaimType == claimType);

            if (claim is null)
            {
                return null;
            }

            return claim.ClaimValue;
        }
    }
}
