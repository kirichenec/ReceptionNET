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
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
            {
                var mainViewModel = Locator.Current.GetService<MainViewModel>();
                var mainView = new MainWindow { DataContext = mainViewModel };
                desktopLifetime.MainWindow = mainView;
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
