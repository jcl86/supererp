using Microsoft.AspNetCore.Identity;

namespace SuperErp.Management.Domain
{
    public class Role : IdentityRole<string>
    {
        private Role() : base() { }
        public Role(string roleName) : base(roleName)
        {
            Id = Guid.NewGuid().ToString();
            NormalizedName = roleName.ToUpper();
        }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
