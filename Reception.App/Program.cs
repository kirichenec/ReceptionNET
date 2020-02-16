using Avalonia;
using Avalonia.Controls;
using Avalonia.Logging.Serilog;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Reception.App.Models;
using Reception.App.Network.Chat;
using Reception.App.Network.Server;
using Reception.App.ViewModels;
using Reception.App.Views;
using Splat;

namespace Reception.App
{
    static class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        public static void Main(string[] args) => BuildAvaloniaApp().Start(AppMain, args);

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
        {
            Locator.CurrentMutable.RegisterConstant(new NetworkService<Person>(AppSettings.ServerPath), typeof(INetworkService<Person>));
            Locator.CurrentMutable.RegisterConstant(new PingService(AppSettings.ServerPath), typeof(IPingService));
            Locator.CurrentMutable.Register(() => new ClientService(AppSettings.ChatServerPath, true), typeof(IClientService));
            Locator.CurrentMutable.Register(() => new SubordinateView(), typeof(IViewFor<SubordinateViewModel>));
            Locator.CurrentMutable.Register(() => new BossView(), typeof(IViewFor<BossViewModel>));

            return
                AppBuilder
                    .Configure<App>()
                    .UsePlatformDetect()
                    .LogToDebug()
                    .UseReactiveUI();
        }

        // Your application's entry point. Here you can initialize your MVVM framework, DI
        // container, etc.
        private static void AppMain(Application app, string[] args)
        {
            var window =
                new MainWindow
                {
                    DataContext = new MainWindowViewModel()
                };

            app.Run(window);
        }
    }
}