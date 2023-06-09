﻿using SuperErp.Core.FunctionalTests;
using SuperErp.Management.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;

namespace SuperErp.Management.FunctionalTests
{
    public static class UserExtensions
    {
        public static async Task<User> UserInDatabase(this ServerFixture given, string password = null)
        {
            var model = UserMother.Register(password);

            var response = await given.Server
              .CreateRequest(Endpoints.Users.Register)
              .WithIdentity(Identities.SuperAdministrator)
              .WithJsonBody(model)
              .PostAsync();

            await response.ShouldBe(StatusCodes.Status200OK);
            var user = await response.ReadJsonResponse<Model.User>();
            return user;
        }
    }
}
