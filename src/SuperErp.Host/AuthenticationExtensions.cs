using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SuperErp.Api.BoundedContexts.Management.Account.Domain;
using SuperErp.Core;
using SuperErp.Management.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SuperErp.Host
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddJwtCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            string apiKeyValue = configuration.GetValue<string>(key: TokenGenerator.ApiKeyConfigurationName);

            var key = Encoding.ASCII.GetBytes(apiKeyValue);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                //var handler = new JwtSecurityTokenHandler();
                //handler.InboundClaimTypeMap.Clear();
                //options.SecurityTokenValidators.Add(handler);
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<User>>();
                        var user = await userManager.FindByIdAsync(context.Principal.GetUserId());

                        if (user == null)
                        {
                            context.Fail("Unauthorized");
                        }
                    }
                };
                //options.RequireHttpsMetadata = environment.EnvironmentName == "Development";
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            return services;
        }
    }
}
