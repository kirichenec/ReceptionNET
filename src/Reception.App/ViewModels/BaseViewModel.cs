using ReactiveUI;
using Reception.Extension;
using System;
using System.Threading.Tasks;
using static Reception.App.ViewModels.IMainViewModel;

namespace Reception.App.ViewModels
{
    public abstract class BaseViewModel : ReactiveObject, IRoutableViewModel
    {
        protected BaseViewModel(string urlPathSegment, ShowErrorAction showError)
        {
            UrlPathSegment = urlPathSegment;

            ThrownExceptions.Subscribe(error => showError(error));
        }

        public IScreen HostScreen { get; }

        public string UrlPathSegment { get; }

        public event Func<Task<bool>> Initialized;

        protected void OnInitialized()
        {
            Initialized?.Invoke();
            Initialized?.GetInvocationList().ForEach(d => Initialized -= (Func<Task<bool>>)d);
        }
    }
}