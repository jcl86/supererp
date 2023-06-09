﻿using SuperErp.Core;
using SuperErp.Management.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SuperErp.Management.FunctionalTests
{
    public static class Identities
    {
        public static IEnumerable<Claim> FromUser(Model.User user)
        {
            var roles = user.Roles.Select(x => new Claim(ClaimTypes.Role, x));
            return new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email)
            }.Concat(roles);
        }

        public static IEnumerable<Claim> SuperAdministrator => new[]
        {
            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, Management.Model.RoleNames.SuperAdministrator)
        };

        public static IEnumerable<Claim> PlainUser => new[]
       {
            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
        };

    }
}
