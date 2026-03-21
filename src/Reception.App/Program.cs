using Avalonia;
using Avalonia.Controls;
using ReactiveUI.Avalonia;
using Reception.App.Extensions;

namespace Reception.App
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            IocRegister.RegisterDependencies();

            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args, ShutdownMode.OnMainWindowClose);
        }

        public static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder
                .Configure<App>()
                .UsePlatformDetect()
                .UsePlatformCrutches()
                .LogToTrace()
                .UseReactiveUI(rxui => { });
        }
    }
}
