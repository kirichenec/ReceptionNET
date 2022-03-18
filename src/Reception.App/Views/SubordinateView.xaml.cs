using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Reception.App.ViewModels;

namespace Reception.App.Views
{
    public class SubordinateView : BaseControl<SubordinateViewModel>
    {
        public SubordinateView()
        {
            AvaloniaXamlLoader.Load(this);

            InitFirstFocusItem<TextBox>();
        }
    }
}