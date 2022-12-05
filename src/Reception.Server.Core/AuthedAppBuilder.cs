using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Reception.Server.Auth.ConnectionLibrary;
using Reception.Server.Core.Extensions;

namespace Reception.Server.Core
{
    public static class AuthedAppBuilder
    {
        public static void BuildAndRunApp(Type appType, Action<WebApplicationBuilder> configureServices, string[] args)
        {
            BaseAppBuilder.BuildAndRunApp(() => BaseAppBuilder.AppBuilder(appType, configureServices, Configure, args));
        }

        private static void Configure(WebApplication app, WebApplicationBuilder builder)
        {
            BaseAppBuilder.Configure(app, builder);

            app.MapDefaultControllerRoute();
        }

        public static void ConfigureServices(WebApplicationBuilder builder, string openApiTitle)
        {
            BaseAppBuilder.ConfigureServices(builder, openApiTitle);

            builder.Services.ConfigureSwaggerAdditional<AuthorizeAttribute>();

            // configure strongly typed settings object
            builder.Services.ConfigureOptions<AuthSettings>(builder.Configuration);
        }
    }
}
