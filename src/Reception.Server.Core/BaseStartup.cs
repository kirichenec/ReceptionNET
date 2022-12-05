using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Reception.Server.Core
{
    public class BaseStartup
    {
        public const string DEFAULT_OPEN_API_VERSION = "v1";
        public const string DEFAULT_SWAGGER_URL = $"/swagger/{DEFAULT_OPEN_API_VERSION}/swagger.json";

        public BaseStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public virtual void ConfigureCoreServices(IServiceCollection services, string openApiTitle,
            string openApiVersion = DEFAULT_OPEN_API_VERSION)
        {
            services.AddMvc().AddNewtonsoftJson();

            services.AddSwaggerGen(swaggerOptions =>
            {
                swaggerOptions.SwaggerDoc(openApiVersion, new OpenApiInfo { Title = openApiTitle, Version = openApiVersion });
                swaggerOptions.EnableAnnotations();
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        protected void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<BaseStartup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint(DEFAULT_SWAGGER_URL, env.ApplicationName));

            app.UseHttpsRedirection();
            app.UseRouting();

            logger.LogInformation("{appName} started", Configuration.GetValue<string>("appName"));
            logger.LogInformation("Swagger: {swaggerUrl}", DEFAULT_SWAGGER_URL);
        }
    }
}
