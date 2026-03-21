using Avalonia;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using ReactiveUI.Avalonia;
using Reception.App.ViewModels;
using System.Reactive.Disposables;
using System.Reactive.Disposables.Fluent;

namespace Reception.App.Views
{
    public partial class MainWindow : ReactiveWindow<MainViewModel>
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
