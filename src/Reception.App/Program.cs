using Avalonia;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using Reception.App.Extensions;

namespace Reception.App
{
    static class Program
    {
        public static AppBuilder BuildAvaloniaApp()
        {
            IocRegister.RegisterDependencies();

            return AppBuilder
                .Configure<App>()
                .UsePlatformDetect()
                .UsePlatformCrutches()
                .LogToTrace()
                .UseReactiveUI();
        }

        public static void Main(string[] args)
        {
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args, ShutdownMode.OnMainWindowClose);
        }
    }
}
