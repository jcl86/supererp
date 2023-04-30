using SuperErp.Core.FunctionalTests;
using SuperErp.Core.Model;
using SuperErp.Management.Domain;
using SuperErp.Management.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bogus;
using SuperErp.Sales.Model;

namespace SuperErp.Management.FunctionalTests
{
    public static class LoginExtensions
    {
        public static async Task SuccessToLogin(this ServerFixture given, string email, string password)
        {
            var response = await given
             .Server
             .CreateRequest(Model.Endpoints.Accounts.Login)
             .WithJsonBody(new LoginRequest()
             {
                 Email = email,
                 Password = password,
             })
             .PostAsync();

            await response.ShouldBe(StatusCodes.Status200OK);
            var result = await response.ReadJsonResponse<AuthenticationSuccessResponse>();
            result.Username.Should().Be(email);
            result.Token.Should().NotBeEmpty();
        }

        public static async Task FailToLogin(this ServerFixture given, string email, string password)
        {
            var response = await given
             .Server
             .CreateRequest(Model.Endpoints.Accounts.Login)
             .WithJsonBody(new LoginRequest()
             {
                 Email = email,
                 Password = password,
             })
             .PostAsync();

            await response.ShouldBe(StatusCodes.Status401Unauthorized);
            var result = await response.ReadJsonResponse<ProblemDetails>();
            result.Status.Should().Be(StatusCodes.Status401Unauthorized);
        }
    }
}
