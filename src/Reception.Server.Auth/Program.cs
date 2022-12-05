using Reception.Server.Auth.Helpers;
using Reception.Server.Auth.Logic;
using Reception.Server.Auth.Model;
using Reception.Server.Auth.PasswordHelper;
using Reception.Server.Auth.Repository;
using Reception.Server.Core;
using Reception.Server.Core.Extensions;

BaseAppBuilder.BuildAndRunApp(typeof(Program), ConfigureServices, Configure, args);

void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddCors();

    BaseAppBuilder.ConfigureServices(builder, "Reception auth server");

    builder.Services.ConfigureSwaggerAdditional<InternalServerAuthorizeAttribute>();

    // configure strongly typed settings object
    builder.Services.ConfigureOptions<AppSettings>(builder.Configuration);
    builder.Services.ConfigureOptions<HashingOptions>(builder.Configuration);

    // configure DI for application services
    builder.Services.AddEntityFrameworkSqlite().AddDbContext<AuthContext>();
    builder.Services.AddScoped<ITokenService, TokenService>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IUserLogic, UserLogic>();
}

void Configure(WebApplication app, WebApplicationBuilder builder)
{
    BaseAppBuilder.Configure(app, builder);

    // global cors policy
    app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader());

    // custom jwt auth middleware
    app.UseMiddleware<JwtMiddleware>();

    app.MapControllers();
}
