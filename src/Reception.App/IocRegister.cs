using Reception.App.Network.Auth;
using Reception.App.Network.Chat;
using Reception.App.Network.Server;
using Reception.App.Service;
using Reception.App.Service.Interface;
using Reception.App.ViewModels;
using Reception.App.Views;
using Splat;

namespace Reception.App
{
    internal static class IocRegister
    {
        internal static void RegisterDependencies()
        {
            SplatRegistrations.SetupIOC();

            // Static services
            SplatRegistrations.RegisterLazySingleton<ISettingsService, SettingsService>();
            SplatRegistrations.RegisterLazySingleton<IAuthService, AuthService>();
            SplatRegistrations.RegisterLazySingleton<IPingService, PingService>();
            SplatRegistrations.RegisterLazySingleton<IClientService, ClientService>();

            // Services, new each request
            SplatRegistrations.Register<IPersonNetworkService, PersonNetworkService>();
            SplatRegistrations.Register<IFileDataNetworkService, FileDataNetworkService>();

            // View models
            SplatRegistrations.RegisterLazySingleton<MainViewModel>();
            SplatRegistrations.Register<AuthViewModel>();
            SplatRegistrations.Register<BossViewModel>();
            SplatRegistrations.Register<SubordinateViewModel>();

            // Views
            SplatRegistrations.Register<AuthView>();
            SplatRegistrations.Register<BossView>();
            SplatRegistrations.Register<SubordinateView>();
        }
    }
}
