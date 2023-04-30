using SuperErp.Core;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace SuperErp.Management.Domain
{
    [Service]
    public class PasswordChanger
    {
        private readonly UserManager<User> userManager;

        public PasswordChanger(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task Change(ClaimsPrincipal currentUser, string currentPassword, string newPassword)
        {
            string userId = currentUser.GetUserId();
            var user = await userManager.FindByIdAsync(userId);

            if (user is null)
            {
                throw new NotFoundException<User>(userId);
            }

            if (!await userManager.CheckPasswordAsync(user, currentPassword))
            {
                throw new DomainException("Incorrect password");
            }

            var result = await userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!result.Succeeded)
            {
                throw new DomainException(result.Errors.Select(x => x.Description).ToArray());
            }

        }
    }
}
