using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Reception.App.ViewModels;
using Reception.App.Views;
using Splat;

namespace Reception.App
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var mainViewModel = Locator.Current.GetService<MainViewModel>();

            switch (ApplicationLifetime)
            {
                case IClassicDesktopStyleApplicationLifetime desktopLifetime:
                    desktopLifetime.MainWindow = new MainWindow { DataContext = mainViewModel };
                    break;
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
