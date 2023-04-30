using SuperErp.Core.Model;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using System.IdentityModel.Tokens.Jwt;

namespace SuperErp.Core
{
    public static class ApiConfiguration
    {
        public static IServiceCollection ConfigureServices(IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration)
        {
            return services
                .AddControllersFromCurrentProject()
                .AddCustomAspnetIdentity(configuration)
                .AddCustomServices()
                .ConfigureProblemDetails(environment)
                .AddRouting()
                .AddAuthorization(Policies.Configure)
                .CustomizeModelBindingErrorBehaviour()
                .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
                .AddFluentValidationAutoValidation();
        }

        public static IApplicationBuilder Configure(IApplicationBuilder app, Func<IApplicationBuilder, IApplicationBuilder> configureHost)
        {
            return configureHost(app)
                .UseProblemDetails()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapGet(Endpoints.Health, () => "healthy");
                });
        }
    }
}