using Microsoft.OpenApi.Models;
using System.Reflection;

namespace EStatAnalyser.Web.Admin.API.API.Extensions.SwashbuckleExtension
{
    public static class SwashbuckleService
    {
        public static IServiceCollection AddSwachbackleService(
            this IServiceCollection services)
        {
            if (services is null)
            {
                throw new System.ArgumentNullException(nameof(services));
            }
            return services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "Поколение 1",
                    Title = "API",
                    Description = "Backend Web API на C# .NET",
                    Contact = new OpenApiContact
                    {
                        Name = "MySource",
                        Url = new Uri("https://github.com/JorgenSchmidt")
                    }
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }
    }
}