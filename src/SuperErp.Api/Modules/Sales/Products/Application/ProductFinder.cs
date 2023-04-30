using SuperErp.Core;
using System;
using System.Threading.Tasks;

namespace SuperErp.Sales.Domain
{
    [Service]
    public class ProductFinder
    {
        private readonly Data.ApplicationDbContext context;

        public ProductFinder(Data.ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Product> Find(string productId)
        {
            var product = await context.Set<Product>().FindAsync(productId);
            if (product is null)
            {
                throw new NotFoundException<Product>(productId);
            }
            return product;
        }
    }

}
