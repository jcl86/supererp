using Microsoft.AspNetCore.Identity;
using SuperErp.Core;
using SuperErp.Management.Model;
using System.Security.Claims;

namespace SuperErp.Management.Domain
{
    [Service]
    public class UserRegister
    {
        private readonly UserManager<User> userManager;

        public UserRegister(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public string RoleTypes { get; private set; }

        public async Task<User> Create(Model.RegisterUser model)
        {
            var emailAddress = new EmailAddress(model.Email);

            var user = new User() { UserName = emailAddress.ToString(), Email = emailAddress.ToString() };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);
                throw new DomainException($"User {user} could not be created. {string.Join(", ", errors)}");
            }

            await userManager.AddToRoleAsync(user, RoleNames.PlainUser);

            await userManager.AddClaimAsync(user, model.DelegationId.ToClaim(CustomClaimTypes.DelegationId));
            await userManager.AddClaimAsync(user, model.StartDate.ToClaim(CustomClaimTypes.StartDate));

            return user;
        }
    }
}
