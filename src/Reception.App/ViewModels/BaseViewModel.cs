using ReactiveUI;
using Reception.Extension;
using System;
using System.Threading.Tasks;

namespace Reception.App.ViewModels
{
    public abstract class BaseViewModel : ReactiveObject, IRoutableViewModel
    {
        protected BaseViewModel()
        {
            this.ThrownExceptions.Subscribe(error => MainVM.ShowError(error));
        }

        public IScreen HostScreen { get; set; }

        public static MainWindowViewModel MainVM => ViewLocator.MainVM;

        public string UrlPathSegment { get; set; }

        public event Func<Task<bool>> Initialized;

        protected void OnInitialized()
        {
            Initialized?.Invoke();
            Initialized?.GetInvocationList().ForEach(d => Initialized -= (Func<Task<bool>>)d);
        }
    }
}