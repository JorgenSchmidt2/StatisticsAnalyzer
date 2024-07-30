using EStatAnalyser.Web.Admin.API.Core.Interfaces.ServiceInterfaces;
using EStatAnalyser.Web.Admin.API.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EStatAnalyser.Web.Admin.API.Services
{
    public static class Entry
    {
        public static IServiceCollection AddServices(
            this IServiceCollection services)
        {
            if (services is null)
            {
                throw new System.ArgumentNullException(nameof(services));
            }

            services.AddScoped<IDataService, DataService>();

            return services;
        }
    }
}