using Microsoft.EntityFrameworkCore;
using SuperErp.Core;
using SuperErp.Data;
using SuperErp.Management.Model;

namespace SuperErp.Management.Domain
{
    [Service]
    public class UserLister
    {
        private readonly ApplicationDbContext context;

        public UserLister(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Model.User>> GetAll()
        {
            var users = await context.Set<User>()
                .Include(x => x.Claims)
                .Include(x => x.UserRoles)
                    .ThenInclude(x => x.Role)
                .ToListAsync();
            return users.Select(x => x.ToModel());
        }
    }
}
