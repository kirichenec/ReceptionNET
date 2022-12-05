using Reception.Server.Core;
using Reception.Server.Data.Logic;
using Reception.Server.Data.Repository;

AuthedAppBuilder.BuildAndRunApp(typeof(Program), ConfigureServices, args);

static void ConfigureServices(WebApplicationBuilder builder)
{
    AuthedAppBuilder.ConfigureServices(builder, "Reception data server");

    // configure DI for application services
    builder.Services.AddEntityFrameworkSqlite().AddDbContext<DataContext>();
    builder.Services.AddScoped<IDataService, DataService>();
    builder.Services.AddScoped<IPersonLogic, PersonLogic>();
}
