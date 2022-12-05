using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Reception.Server.Core.Extensions;
using Serilog;
using static Reception.Server.Core.Constants.SwaggerConstants;
using static Reception.Server.Core.Extensions.LoggerExtensions;

namespace Reception.Server.Core
{
    public static class BaseAppBuilder
    {
        public static void BuildAndRunApp(Type appType, Action<WebApplicationBuilder> configureServices,
            Action<WebApplication, WebApplicationBuilder> configure, string[] args)
        {
            BuildAndRunAppInternal(() => AppBuilder(appType, configureServices, configure, args));
        }

        internal static void BuildAndRunAppInternal(Action appBuilder)
        {
            Log.Logger = GetConsoleLogger();

            try
            {
                Log.Information("Starting web application");
                appBuilder();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static void AppBuilder(Type appType, Action<WebApplicationBuilder> configureServices,
            Action<WebApplication, WebApplicationBuilder> configure, string[] args)
        {
            AppBuilderInternal(appType, configureServices, configure, args);
        }

        internal static void AppBuilderInternal(Type appType, Action<WebApplicationBuilder> configureServices,
            Action<WebApplication, WebApplicationBuilder> configure, string[] args)
        {
            var appName = appType.Assembly.GetName().Name;

            var builder = WebApplication.CreateBuilder(new WebApplicationOptions
            {
                Args = args,
                ApplicationName = appName,
                ContentRootPath = Directory.GetCurrentDirectory(),
                EnvironmentName = Environments.Staging
            });

            configureServices(builder);

            builder.Host.UseLogger();

            var app = builder.Build();

            configure(app, builder);

            Log.Information("{appName} started", builder.Environment.ApplicationName);
            Log.Information("Swagger: {swaggerUrl}", DEFAULT_SWAGGER_URL);

            app.Run();
        }

        public static void Configure(WebApplication app, WebApplicationBuilder builder)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint(DEFAULT_SWAGGER_URL, builder.Environment.ApplicationName));

            app.UseHttpsRedirection();
            app.UseRouting();
        }

        public static void ConfigureServices(WebApplicationBuilder builder, string openApiTitle)
        {
            builder.Services.AddMvc().AddNewtonsoftJson();

            builder.Services.AddSwaggerGen(swaggerOptions =>
            {
                swaggerOptions.SwaggerDoc(
                    name: DEFAULT_OPEN_API_VERSION,
                    info: new OpenApiInfo { Title = openApiTitle, Version = DEFAULT_OPEN_API_VERSION });
                swaggerOptions.EnableAnnotations();
            });

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
