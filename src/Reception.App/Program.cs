using Avalonia;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Reception.App.Extensions;
using Reception.App.Model.FileInfo;
using Reception.App.Model.PersonInfo;
using Reception.App.Network.Auth;
using Reception.App.Network.Chat;
using Reception.App.Network.Server;
using Reception.App.Service;
using Reception.App.Service.Interface;
using Splat;
using System.Reflection;

namespace Reception.App
{
    static class Program
    {
        public static AppBuilder BuildAvaloniaApp()
        {
            InitLocatorObjects();

            return AppBuilder
                .Configure<App>()
                .UsePlatformDetect()
                .UsePlatformCrutches()
                .LogToTrace()
                .UseReactiveUI();
        }

        private static void InitLocatorObjects()
        {
            // Register a singleton which won't get created until the first user accesses it
            Locator.CurrentMutable.RegisterLazySingleton(() => new SettingsService(), typeof(ISettingsService));
            Locator.CurrentMutable.RegisterLazySingleton(() => new AuthService(), typeof(IAuthService));
            Locator.CurrentMutable.RegisterLazySingleton(() => new PingService(), typeof(IPingService));
            Locator.CurrentMutable.RegisterLazySingleton(() => new ClientService(), typeof(IClientService));

            // Create a new any time someone asks
            Locator.CurrentMutable.Register(() => new PersonNetworkService(), typeof(INetworkService<Person>));
            Locator.CurrentMutable.Register(() => new FileDataNetworkService(), typeof(INetworkService<FileData>));

            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());
        }

        public static void Main(string[] args)
        {
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }
    }
}