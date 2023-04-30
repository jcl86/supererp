using Microsoft.AspNetCore.Identity;
using SuperErp.Api.BoundedContexts.Management.Account.Domain;
using SuperErp.Core;

namespace SuperErp.Management.Domain
{
    [Service]
    public class LoginService
    {
        private readonly UserFinder userFinder;
        private readonly UserManager<User> userManager;
        private readonly TokenGenerator tokenGenerator;

        public LoginService(UserFinder userFinder, UserManager<User> userManager, TokenGenerator tokenGenerator)
        {
            this.userFinder = userFinder;
            this.userManager = userManager;
            this.tokenGenerator = tokenGenerator;
        }

        public async Task<string> GetAuthenticationToken(Model.LoginRequest model)
        {
            var user = await userFinder.FindByEmail(model.Email);
            if (user is null)
            {
                throw new UnauthorizedAccessException(Account.Messages.LoginError);
            }

            if (userManager.SupportsUserLockout && await userManager.IsLockedOutAsync(user))
            {
                throw new UnauthorizedAccessException(Account.Messages.AccountLockedOut);
            }

            var succeded = await userManager.CheckPasswordAsync(user, model.Password);
            if (!succeded)
            {
                throw new UnauthorizedAccessException(Account.Messages.LoginError);
            }

            string token = tokenGenerator.GenerateToken(user);
            return token;
        }
    }
}
