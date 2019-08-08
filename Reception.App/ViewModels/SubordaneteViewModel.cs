using ReactiveUI;

namespace Reception.App.ViewModels
{
    public class SubordinateViewModel : BaseViewModel
    {
        public SubordinateViewModel(IScreen screen)
        {
            UrlPathSegment = nameof(SubordinateViewModel);
            HostScreen = screen;
        }
    }
}
