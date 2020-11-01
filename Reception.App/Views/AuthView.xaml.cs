using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Reception.App.ViewModels;

namespace Reception.App.Views
{
    public class AuthView : ReactiveUserControl<AuthViewModel>
    {
        public AuthView()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}