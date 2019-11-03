using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Reception.App.ViewModels
{
    public class BaseViewModel : ReactiveObject, IRoutableViewModel
    {
        public enum ViewModelState
        {
            Write,
            Read
        }

        [Reactive]
        public ViewModelState State { get; set; }

        public string UrlPathSegment { get; set; }

        public IScreen HostScreen { get; set; }
    }
}
