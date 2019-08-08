using Avalonia;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using Reception.App.ViewModels;

namespace Reception.App.Views
{
    public class BossView : ReactiveUserControl<BossViewModel>
    {
        public BossView()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}
