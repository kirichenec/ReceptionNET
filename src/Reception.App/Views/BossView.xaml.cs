using Avalonia.Markup.Xaml;
using Reception.App.ViewModels;

namespace Reception.App.Views
{
    public class BossView : BaseControl<BossViewModel>
    {
        public BossView()
        {
            AvaloniaXamlLoader.Load(this);

            InitFirstFocusItem();
        }
    }
}