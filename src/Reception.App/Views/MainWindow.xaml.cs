using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Reception.App.ViewModels;
using System.Reactive.Disposables;

namespace Reception.App.Views
{
    public class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            AvaloniaXamlLoader.Load(this);
#if DEBUG
            this.AttachDevTools();
#endif
            this.WhenActivated(disposables =>
            {
                Disposable.Create(() => { }).DisposeWith(disposables);
            });
        }
    }
}