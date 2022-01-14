using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reception.Model.Interface;
using Reception.Server.Auth.Helpers;
using Reception.Server.Auth.Logic;
using Reception.Server.Auth.Model;
using Reception.Server.Auth.PasswordHelper;
using Reception.Server.Auth.Repository;
using Reception.Server.Core;
using Reception.Server.Core.Extensions;

namespace Reception.Server.Auth
{
    public class Startup : BaseStartup
    {
        public Startup(IConfiguration configuration) : base(configuration) { }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            ConfigureCoreServices(services, openApiTitle: "Reception auth server");

            services.ConfigureSwaggerAdditional<InternalServerAuthorizeAttribute>();

            // configure strongly typed settings object
            services.ConfigureOptions<AppSettings>(Configuration);
            services.ConfigureOptions<HashingOptions>(Configuration);

            // configure DI for application services
            services.AddEntityFrameworkSqlite().AddDbContext<AuthContext>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserLogic, UserLogic>();
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.Configure(app, env);

            // global cors policy
            app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader());

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}