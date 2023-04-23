using Avalonia.Markup.Xaml;
using Reception.App.ViewModels;

namespace Reception.App.Views
{
    public partial class SubordinateView : BaseControl<SubordinateViewModel>
    {
        public SubordinateView()
        {
            AvaloniaXamlLoader.Load(this);

            InitFirstFocusItem();
        }
    }
}