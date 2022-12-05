using Reception.Server.Core;
using Reception.Server.Core.Extensions;
using Reception.Server.File.Logic;
using Reception.Server.File.Model;
using Reception.Server.File.Repository;


AuthedAppBuilder.BuildAndRunApp(typeof(Program), ConfigureServices, args);

static void ConfigureServices(WebApplicationBuilder builder)
{
    AuthedAppBuilder.ConfigureServices(builder, "Reception files server");

    builder.Services.ConfigureOptions<AppSettings>(builder.Configuration);

    // configure DI for application services
    builder.Services.AddEntityFrameworkSqlite().AddDbContext<FileContext>();
    builder.Services.AddScoped<IFileDataService, FileDataService>();
    builder.Services.AddScoped<IFileDataLogic, FileDataLogic>();
}
