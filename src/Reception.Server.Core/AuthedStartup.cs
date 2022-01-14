using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reception.Server.Auth.ConnectionLibrary;
using Reception.Server.Core.Extensions;

namespace Reception.Server.Core
{
    public class AuthedStartup : BaseStartup
    {
        public AuthedStartup(IConfiguration configuration) : base(configuration) { }

        public override void ConfigureCoreServices(IServiceCollection services, string openApiTitle,
            string openApiVersion = DEFAULT_OPEN_API_VERSION)
        {
            base.ConfigureCoreServices(services, openApiTitle, openApiVersion);

            services.ConfigureSwaggerAdditional<AuthorizeAttribute>();

            // configure strongly typed settings object
            services.ConfigureOptions<AuthSettings>(Configuration);
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.Configure(app, env);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}