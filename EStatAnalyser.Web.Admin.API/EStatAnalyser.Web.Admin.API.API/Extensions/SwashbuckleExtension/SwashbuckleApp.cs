namespace EStatAnalyser.Web.Admin.API.API.Extensions.SwashbuckleExtension
{
    public static class SwashbuckleApp
    {
        public static IApplicationBuilder SwaggerApp(this IApplicationBuilder app)
        {
            if (app is null)
            {
                throw new System.ArgumentNullException(nameof(app));
            }
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
            return app;
        }
    }
}