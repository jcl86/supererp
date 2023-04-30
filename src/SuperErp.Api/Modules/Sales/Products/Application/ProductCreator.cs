using SuperErp.Core;
using Microsoft.EntityFrameworkCore;

namespace SuperErp.Sales.Domain
{
    [Service]
    public class ProductCreator
    {
        private readonly Data.ApplicationDbContext context;

        public ProductCreator(Data.ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Product> Create(Model.CreateProduct dto)
        {
            if (await CodeAlreadyExists(dto.Code))
            {
                throw new DomainException(Products.Messages.ProductCodeAlreadyExists);
            }

            var product = new Product(dto.Code, dto.Name, dto.Description, dto.Price);
            context.Add(product);
            await context.SaveChangesAsync();
            return product;
        }

        private async Task<bool> CodeAlreadyExists(string code) => await context.Set<Product>().AnyAsync(x => x.Code == code);
    }
}
