using Bogus;
using SuperErp.Core.FunctionalTests;
using SuperErp.Sales.Model;

namespace SuperErp.Sales.FunctionalTests
{
    public static class ProductMother
    {
        public static CreateProduct Create()
        {
            var faker = new Faker<CreateProduct>()
               .StrictMode(true)
               .RuleFor(x => x.Code, f => f.Random.AlphaNumeric(8).ToUpper())
               .RuleFor(x => x.Name, f => f.Commerce.Product().Cut(Restrictions.Products.NameMaxLength))
               .RuleFor(x => x.Description, f => f.Commerce.ProductDescription().Cut(Restrictions.Products.DescriptionMaxLength))
               .RuleFor(x => x.Price, f => f.Finance.Amount(1, 100));

            return faker.Generate();
        }

        public static UpdateProduct Update()
        {
            var faker = new Faker<UpdateProduct>()
               .StrictMode(true)
               .RuleFor(x => x.Name, f => f.Commerce.Product().Cut(Restrictions.Products.NameMaxLength))
               .RuleFor(x => x.Description, f => f.Commerce.ProductDescription().Cut(Restrictions.Products.DescriptionMaxLength))
               .RuleFor(x => x.Price, f => f.Finance.Amount(1, 100));

            return faker.Generate();
        }
    }
}
