using Reception.App.Network.Chat;
using Reception.Model.Network;
using Reception.Server.Core;

namespace Reception.Server.Chat
{
    public class Startup : BaseStartup
    {
        public Startup(IConfiguration configuration) : base(configuration) { }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSignalR().AddNewtonsoftJsonProtocol();

            ConfigureCoreServices(services, "Reception chat server");
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.Configure(app, env);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub<QueryResult<object>>>("/ChatHub");
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}