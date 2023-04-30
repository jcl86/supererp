using SuperErp.Management.Model;
using Microsoft.AspNetCore.Authorization;

namespace SuperErp.Core
{
    public static class Policies
    {
        public const string IsSuperAdministrator = nameof(IsSuperAdministrator);

        public static void Configure(AuthorizationOptions options)
        {
            options.AddPolicy(IsSuperAdministrator, policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.RequireRole(RoleNames.SuperAdministrator);
            });
        }
    }
}