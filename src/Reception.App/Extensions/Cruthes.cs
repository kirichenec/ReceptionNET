using Avalonia;

namespace Reception.App.Extensions
{
    internal static class Cruthes
    {
        public static AppBuilder UsePlatformCrutches(this AppBuilder appBuilder)
        {
            return appBuilder
                .With(new X11PlatformOptions
                {
                    EnableMultiTouch = true,
                    UseDBusMenu = true,
                    EnableIme = true
                })
                .With(new Win32PlatformOptions
                {
                    // Avalonia 11.0.0-preview1 issue: CornerRadius not clipping,
                    // Avalonia 11.0.0-preview1 issue: sometimes might crash by collection enumerate fail
                    UseCompositor = false
                })
                .With(new X11PlatformOptions
                {
                    UseCompositor = false
                })
                .With(new AvaloniaNativePlatformOptions
                {
                    UseCompositor = false
                });
        }
    }
}
