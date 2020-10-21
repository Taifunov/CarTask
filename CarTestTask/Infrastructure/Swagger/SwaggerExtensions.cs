using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CarTestTask.Infrastructure.Swagger
{
    public static class SwaggerExtensions
    {
        public static void AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                const string docVersion = "v1";
                c.SwaggerDoc(docVersion, new OpenApiInfo
                {
                    Title = configuration["ApiName"],
                    Version = docVersion
                });
                c.CustomSchemaIds(s => s.FullName);
            });
        }

        public static void UseSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = configuration["ApiName"];
                c.RoutePrefix = "swagger";
                c.SwaggerEndpoint("v1/swagger.json", "Specification 1");
            });
        }
    }
}
