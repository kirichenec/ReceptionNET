using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Reception.Server.Auth.ConnectionLibrary;
using Reception.Server.Core.Extensions;

namespace Reception.Server.Core
{
    public class StartupCore
    {

        static StartupCore()
        {
            ReceptionLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
        }

        public StartupCore(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static ILoggerFactory ReceptionLoggerFactory { get; }

        public IConfiguration Configuration { get; }

        public void ConfigureCoreServices(IServiceCollection services, string openApiTitle, string openApiVersion = "v1")
        {
            services.AddMvc().AddNewtonsoftJson();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(openApiVersion, new OpenApiInfo { Title = openApiTitle, Version = openApiVersion });
            });

            // configure strongly typed settings object
            services.ConfigureOptions<AuthSettings>(Configuration);
        }

        public static void CoreConfigure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication1 v1"));
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}