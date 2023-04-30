using Microsoft.Net.Http.Headers;
using SuperErp.Core;

namespace SuperErp.Host
{
    public class Startup
    {
        private readonly IWebHostEnvironment environment;
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.configuration = configuration;
            this.environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ApiConfiguration.ConfigureServices(services, environment, configuration)
                .AddJwtCustomAuthentication(configuration);
            services.AddCors();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app)
        {
            ApiConfiguration.Configure(app, host =>
            {
                if (environment.IsDevelopment())
                {
                    host.UseSwagger();
                    host.UseSwaggerUI();
                }

                var allowedOrigins = configuration.GetSection("AllowedOrigins").Get<IEnumerable<string>>();

                host.UseCors(policy =>
                         policy.WithOrigins(allowedOrigins.ToArray())
                         .AllowAnyMethod()
                         .WithHeaders(HeaderNames.ContentType, HeaderNames.Authorization)
                         .AllowCredentials());

                return host;
            });
        }
    }
}
