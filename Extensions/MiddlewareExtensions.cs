using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace InsuranceSolution.API.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(cfg =>
            {
                cfg.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "InsuranceSolution API",
                    Version = "v3",
                    Description = "Simple RESTful API built with ASP.NET Core 3.1 to show how to create RESTful services using a decoupled, maintainable architecture.",
                    Contact = new OpenApiContact
                    {
                        Name = "InsuranceSolution",
                        Url = new Uri("https://localhost:44341/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "InsuranceSolution",
                    },
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                cfg.IncludeXmlComments(xmlPath);
            });
            return services;
        }

        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger().UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "InsuranceSolution  API");
                options.DocumentTitle = "InsuranceSolution API";
            });
            return app;
        }
    }
}
