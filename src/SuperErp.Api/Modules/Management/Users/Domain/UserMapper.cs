using SuperErp.Core;
using SuperErp.Management.Model;

namespace SuperErp.Management.Domain
{
    public static class UserMapper
    {
        public static Model.User ToModel(this User user)
        {
            return new Model.User()
            {
                Email = user.Email,
                Id = user.Id,
                DelegationId = user.GetClaim<int>(CustomClaimTypes.DelegationId),
                StartDate = user.GetClaim<DateTime>(CustomClaimTypes.StartDate),
                Roles = user.RoleNames
            };
        }
    }
}
