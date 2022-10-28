using Avalonia.Markup.Xaml;
using Reception.App.ViewModels;

namespace Reception.App.Views
{
    public class AuthView : BaseControl<AuthViewModel>
    {
        public AuthView()
        {
            AvaloniaXamlLoader.Load(this);

            InitFirstFocusItem();
        }
    }
}