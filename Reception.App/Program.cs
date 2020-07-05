using Avalonia;
using Avalonia.Controls;
using Avalonia.Logging.Serilog;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Reception.App.Model.FileInfo;
using Reception.App.Model.PersonInfo;
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
            InitLocatorObjects();

            return
                AppBuilder
                    .Configure<App>()
                    .UsePlatformDetect()
                    .LogToDebug()
                    .UseReactiveUI();
        }

        private static void InitLocatorObjects()
        {
            Locator.CurrentMutable.RegisterConstant(new NetworkService<Person>(AppSettings.DataServerPath), typeof(INetworkService<Person>));
            Locator.CurrentMutable.RegisterConstant(new NetworkService<FileData>(AppSettings.FileServerPath), typeof(INetworkService<FileData>));
            Locator.CurrentMutable.RegisterConstant(new PingService(AppSettings.DataServerPath), typeof(IPingService));
            //TODO: change 333 to normal userId
            Locator.CurrentMutable.Register(() => new ClientService(333, AppSettings.ChatServerPath, true), typeof(IClientService));
            Locator.CurrentMutable.Register(() => new SubordinateView(), typeof(IViewFor<SubordinateViewModel>));
            Locator.CurrentMutable.Register(() => new BossView(), typeof(IViewFor<BossViewModel>));
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