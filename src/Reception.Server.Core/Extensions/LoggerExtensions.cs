using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;

namespace Reception.Server.Core.Extensions
{
    public static class LoggerExtensions
    {
        public static Logger GetConsoleLogger()
        {
            return new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }

        public static IHostBuilder UseLogger(this IHostBuilder webHostBuilder)
        {
            return webHostBuilder
                .UseSerilog((context, services, configuration) => configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext()
                    .WriteTo.Console())
                ;
        }

        public static IHostBuilder UseLogger(this ConfigureHostBuilder webHostBuilder)
        {
            return webHostBuilder.UseSerilog((context, services, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
                .WriteTo.Console())
                ;
        }

        public static void WrapStartup(Action startupAction)
        {
            try
            {
                Log.Information("Starting web host");
                startupAction.Invoke();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
