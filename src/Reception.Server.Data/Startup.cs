using Reception.Server.Core;
using Reception.Server.Data.Logic;
using Reception.Server.Data.Repository;

namespace Reception.Server.Data
{
    public class Startup : AuthedStartup
    {
        public Startup(IConfiguration configuration) : base(configuration) { }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureCoreServices(services, openApiTitle: "Reception data server");

            // configure DI for application services
            services.AddEntityFrameworkSqlite().AddDbContext<DataContext>();
            services.AddScoped<IDataService, DataService>();
            services.AddScoped<IPersonLogic, PersonLogic>();
        }
    }
}