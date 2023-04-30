using Acheve.AspNetCore.TestHost.Security;
using Acheve.TestHost;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SuperErp.Core.FunctionalTests
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.configuration = configuration;
            this.environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ApiConfiguration.ConfigureServices(services, environment, configuration)
                .AddAuthentication(TestServerDefaults.AuthenticationScheme)
                .AddTestServer(options =>
                {
                    options.Events = new TestServerEvents
                    {
                        OnMessageReceived = context => 
                        {
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = context =>
                        {
                            return Task.CompletedTask;
                        },
                        OnChallenge = context =>
                        {
                            return Task.CompletedTask;
                        },
                    };
                });
        }

        public void Configure(IApplicationBuilder app)
        {
            ApiConfiguration.Configure(app, host => host);
        }
    }
}
