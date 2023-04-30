using SuperErp.Core;
using Microsoft.EntityFrameworkCore;

namespace SuperErp.Sales.Domain
{
    [Service]
    public class ProductExists
    {
        private readonly Data.ApplicationDbContext context;

        public ProductExists(Data.ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> WithCode(string code)
        {
            var exists = await context.Set<Product>().AnyAsync(x => x.Code == code);
            return exists;
        }
    }
}
