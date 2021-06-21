using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Reception.Server.Auth.ConnectionLibrary;
using Reception.Server.File.Logic;
using Reception.Server.File.Repository;

namespace Reception.Server.File
{
    public class Startup
    {
        public static readonly ILoggerFactory ReceptionLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddNewtonsoftJson();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Reception files server", Version = "v1" });
            });

            // configure strongly typed settings object
            services.Configure<AuthSettings>(Configuration.GetSection("AuthSettings"));

            // configure DI for application services
            services.AddEntityFrameworkSqlite().AddDbContext<FileContext>();
            services.AddScoped<IFileDataService, FileDataService>();
            services.AddScoped<IFileDataLogic, FileDataLogic>();
            services.AddScoped<IFileVersionService, FileVersionService>();
            services.AddScoped<IFileVersionLogic, FileVersionLogic>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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