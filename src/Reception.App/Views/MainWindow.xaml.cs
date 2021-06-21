using Avalonia.Markup.Xaml;
using ReactiveUI;
using Reception.App.ViewModels;

namespace Reception.App.Views
{
    public class MainWindow : BaseWindow<MainWindowViewModel>
    {
        public MainWindow() : base(false)
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}