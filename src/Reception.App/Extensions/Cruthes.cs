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
                    EnableIme = true,
                    EnableMultiTouch = true,
                    UseDBusMenu = true,
                })
                ;
        }
    }
}
