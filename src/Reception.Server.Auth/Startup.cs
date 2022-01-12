using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Reception.Model.Interface;
using Reception.Server.Auth.Helpers;
using Reception.Server.Auth.Logic;
using Reception.Server.Auth.Model;
using Reception.Server.Auth.PasswordHelper;
using Reception.Server.Auth.Repository;
using Reception.Server.Core.Extensions;

namespace Reception.Server.Auth
{
    public class Startup
    {
        public static readonly ILoggerFactory ReceptionLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddMvc().AddNewtonsoftJson();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Reception auth server", Version = "v1" });
            });

            // configure strongly typed settings object
            services.ConfigureOptions<AppSettings>(Configuration);
            services.ConfigureOptions<HashingOptions>(Configuration);

            // configure DI for application services
            services.AddEntityFrameworkSqlite().AddDbContext<UserContext>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserLogic, UserLogic>();
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

            // global cors policy
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}