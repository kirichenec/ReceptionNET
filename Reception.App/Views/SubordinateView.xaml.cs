using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Reception.App.ViewModels;

namespace Reception.App.Views
{
    public class SubordinateView : ReactiveUserControl<SubordinateViewModel>
    {
        public SubordinateView()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}
