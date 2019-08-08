using ReactiveUI;

namespace Reception.App.ViewModels
{
    public class BossViewModel : BaseViewModel
    {
        public BossViewModel(IScreen screen)
        {
            UrlPathSegment = nameof(BossViewModel);
            HostScreen = screen;
        }
    }
}
