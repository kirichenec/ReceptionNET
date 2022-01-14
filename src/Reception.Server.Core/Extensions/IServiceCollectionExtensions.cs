using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Reception.Constant;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Reception.Server.Core.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void ConfigureOptions<T>(this IServiceCollection services, IConfiguration configuration) where T : class
        {
            services.Configure<T>(configuration.GetSection(typeof(T).Name));
        }

        public static void ConfigureSwaggerAdditional<T>(this IServiceCollection services) where T : Attribute
        {
            services.Configure<SwaggerGenOptions>(swaggerOptions =>
            {
                swaggerOptions.OperationFilter<AppendAuthorizeToSummaryOperationFilter<T>>();

                swaggerOptions.AddSecurityDefinition(
                    HttpHeaders.TOKEN,
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please insert JWT with Bearer into field",
                        Name = HttpHeaders.TOKEN,
                        Type = SecuritySchemeType.ApiKey
                    });
                swaggerOptions.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = HttpHeaders.TOKEN
                                }
                            },
                            Array.Empty<string>()
                        }
                    });
            });
            services.AddSwaggerGenNewtonsoftSupport();
        }
    }
}