using SuperErp.Core.FunctionalTests;
using SuperErp.Sales.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Microsoft.AspNetCore.TestHost;
using SuperErp.Management.FunctionalTests;

namespace SuperErp.Sales.FunctionalTests
{
    [Collection(nameof(ServerFixtureCollection))]
    public class ProductShould
    {
        private readonly ServerFixture Given;

        public ProductShould(ServerFixture fixture)
        {
            Given = fixture ?? throw new ArgumentNullException(nameof(fixture));
        }

        [Fact]
        public async Task Be_found_after_created()
        {
            var product = await Given.ProductInDatabase();

            var response = await Given.Server
               .CreateRequest(Endpoints.Products.Get(product.Id))
               .WithIdentity(Identities.SuperAdministrator)
               .GetAsync();

            await response.ShouldBe(StatusCodes.Status200OK);

            var searched = await response.ReadJsonResponse<Product>();
            searched.Should().BeEquivalentTo(product);
        }

        [Fact]
        public async Task Be_created()
        {
            var dto = ProductMother.Create();

            var response = await Given
            .Server
                .CreateRequest(Endpoints.Products.Create)
                .WithJsonBody(dto)
                .PostAsync();

            await response.ShouldBe(StatusCodes.Status201Created);

            var searched = await response.ReadJsonResponse<Product>();

            searched.Id.Should().NotBeEmpty();
            searched.Code.Should().Be(dto.Code);
            searched.Name.Should().Be(dto.Name);
            searched.Description.Should().Be(dto.Description);
            searched.Price.Should().Be(dto.Price);
        }

        [Fact]
        public async Task Be_updated()
        {
            var product = await Given.ProductInDatabase();

            var dto = ProductMother.Update();

            var response = await Given
            .Server
                .CreateRequest(Endpoints.Products.Update(product.Id))
                .WithJsonBody(dto)
                .PatchAsync();
            await response.ShouldBe(StatusCodes.Status204NoContent);

            response = await Given.Server
               .CreateRequest(Endpoints.Products.Get(product.Id))
               .GetAsync();

            await response.ShouldBe(StatusCodes.Status200OK);
            var searched = await response.ReadJsonResponse<Product>();

            searched.Should().NotBeEquivalentTo(product);

            searched.Id.Should().NotBeEmpty();
            searched.Code.Should().Be(product.Code);
            searched.Name.Should().Be(dto.Name);
            searched.Description.Should().Be(dto.Description);
            searched.Price.Should().Be(dto.Price);
        }

        [Fact]
        public async Task Fail_to_update_when_does_not_exist()
        {
            var nonExistingEntityId = Guid.NewGuid().ToString();
            var dto = ProductMother.Update();

            var response = await Given.Server
               .CreateRequest(Endpoints.Products.Update(nonExistingEntityId))
               .WithJsonBody(dto)
               .DeleteAsync();

            await response.ShouldBe(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task Fail_to_create_with_existing_code()
        {
            var product = await Given.ProductInDatabase();

            var dto = ProductMother.Create();
            dto.Code = product.Code;

            var response = await Given.Server
                .CreateRequest(Endpoints.Products.Create)
                .WithJsonBody(dto)
                .PostAsync();

            await response.ShouldBe(StatusCodes.Status400BadRequest);

            var problem = await response.ReadJsonResponse<ProblemDetails>();
            problem.Detail.Should().Be(Domain.Products.Messages.ProductCodeAlreadyExists);
        }

        [Fact]
        public async Task Be_deleted()
        {
            var product = await Given.ProductInDatabase();

            var response = await Given.Server
               .CreateRequest(Endpoints.Products.Delete(product.Id))
               .DeleteAsync();
            await response.ShouldBe(StatusCodes.Status204NoContent);

            response = await Given.Server
               .CreateRequest(Endpoints.Products.Get(product.Id))
               .GetAsync();

            await response.ShouldBe(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task Fail_to_delete_when_does_not_exist()
        {
            var nonExistingEntityId = Guid.NewGuid().ToString();

            var response = await Given.Server
               .CreateRequest(Endpoints.Products.Delete(nonExistingEntityId))
               .DeleteAsync();

            await response.ShouldBe(StatusCodes.Status404NotFound);
        }
    }
}
