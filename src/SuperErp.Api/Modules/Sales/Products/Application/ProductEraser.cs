using SuperErp.Core;

namespace SuperErp.Sales.Domain
{
    [Service]
    public class ProductEraser
    {
        private readonly ProductFinder finder;
        private readonly Data.ApplicationDbContext context;

        public ProductEraser(ProductFinder finder, Data.ApplicationDbContext context)
        {
            this.finder = finder;
            this.context = context;
        }

        public async Task Delete(string productId)
        {
            var product = await finder.Find(productId);
            context.Set<Product>().Remove(product);
            await context.SaveChangesAsync();
        }
    }
}
