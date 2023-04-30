using SuperErp.Core.FunctionalTests;
using SuperErp.Sales.Model;
using Microsoft.AspNetCore.Http;

namespace SuperErp.Sales.FunctionalTests
{
    public static class ProductExtensions
    {
        public static async Task<Product> ProductInDatabase(this ServerFixture given)
        {
            var dto = ProductMother.Create();

            var response = await given
                .Server
                .CreateRequest(Endpoints.Products.Create)
                .WithJsonBody(dto)
                .PostAsync();

            await response.ShouldBe(StatusCodes.Status201Created);

            var result = await response.ReadJsonResponse<Product>();
            return result;
        }

        public static async Task<Product> FindProductFromDatabase(this ServerFixture given, string productId)
        {
            var response = await given
                .Server
                .CreateRequest(Endpoints.Products.Get(productId))
                .GetAsync();

            await response.ShouldBe(StatusCodes.Status200OK);
            var result = await response.ReadJsonResponse<Product>();
            return result;
        }
    }
}
