using Reception.Model.Network;
using Reception.Server.Chat;
using Reception.Server.Core;

BaseAppBuilder.BuildAndRunApp(typeof(Program), ConfigureServices, Configure, args);

void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddControllers();
    builder.Services.AddSignalR().AddNewtonsoftJsonProtocol();

    BaseAppBuilder.ConfigureServices(builder, "Reception chat server");
}

void Configure(WebApplication app, WebApplicationBuilder builder)
{
    BaseAppBuilder.Configure(app, builder);

    app.UseAuthorization();

    app.MapHub<ChatHub<QueryResult<object>>>("/ChatHub");
    app.MapDefaultControllerRoute();
}
