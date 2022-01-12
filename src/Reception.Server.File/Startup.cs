using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reception.Server.Core;
using Reception.Server.Core.Extensions;
using Reception.Server.File.Logic;
using Reception.Server.File.Model;
using Reception.Server.File.Repository;

namespace Reception.Server.File
{
    public class Startup : StartupCore
    {
        public Startup(IConfiguration configuration) : base(configuration) { }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureCoreServices(services, openApiTitle: "Reception files server");

            services.ConfigureOptions<AppSettings>(Configuration);

            // configure DI for application services
            services.AddEntityFrameworkSqlite().AddDbContext<FileContext>();
            services.AddScoped<IFileDataService, FileDataService>();
            services.AddScoped<IFileDataLogic, FileDataLogic>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) => CoreConfigure(app, env);
    }
}