using SuperErp.Core;
using SuperErp.Sales.Domain;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CustomServicesExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            var serviceTypes = new List<Assembly>()
            {
               typeof(ProductCreator).Assembly
            }
             .SelectMany(x => x.GetTypes())
             .Where(t => Attribute.IsDefined(t, typeof(ServiceAttribute)));

            foreach (var serviceType in serviceTypes)
            {
                services.AddScoped(serviceType);
            }
            return services;
        }
    }


}