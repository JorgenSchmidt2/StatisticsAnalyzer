using EStatAnalyser.Web.Admin.API.Core.Interfaces.RepositoryInterfaces;
using EStatAnalyser.Web.Admin.API.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EStatAnalyser.Web.Admin.API.DAL
{
    public static class Entry
    {
        public static IServiceCollection AddDatabase(
            this IServiceCollection services, string connectionString)
        {
            if (services is null)
            {
                throw new System.ArgumentNullException(nameof(services));
            }

            services.AddDbContext<EfContext>(
                opt =>
                {
                    opt.UseNpgsql(connectionString);
                    opt.UseLazyLoadingProxies();
                }
            );
            services.AddScoped<IDataRepository, DataRepository>();

            return services;
        }
    }
}