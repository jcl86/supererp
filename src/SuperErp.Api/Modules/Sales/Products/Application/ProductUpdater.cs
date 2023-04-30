using SuperErp.Core;

namespace SuperErp.Sales.Domain
{
    [Service]
    public class ProductUpdater
    {
        private readonly ProductFinder finder;
        private readonly Data.ApplicationDbContext context;

        public ProductUpdater(ProductFinder finder, Data.ApplicationDbContext context)
        {
            this.finder = finder;
            this.context = context;
        }

        public async Task Update(string productId, Model.UpdateProduct dto)
        {
            var product = await finder.Find(productId);
            product.UpdateName(dto.Name);
            product.UpdatePrice(dto.Price);
            product.UpdateDescription(dto.Description);
            await context.SaveChangesAsync();
        }
    }
}
