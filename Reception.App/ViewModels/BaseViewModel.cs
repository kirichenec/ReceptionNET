using ReactiveUI;

namespace Reception.App.ViewModels
{
    public class BaseViewModel : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment { get; set; }

        public IScreen HostScreen { get; set; }
    }
}
