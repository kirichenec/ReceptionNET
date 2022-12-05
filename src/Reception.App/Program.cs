using Avalonia;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using Reception.App;
using Reception.App.Extensions;

IocRegister.RegisterDependencies();

AppBuilder
    .Configure<App>()
    .UsePlatformDetect()
    .UsePlatformCrutches()
    .LogToTrace()
    .UseReactiveUI()
    .StartWithClassicDesktopLifetime(args, ShutdownMode.OnMainWindowClose);
