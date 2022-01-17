using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reception.App.Enums;
using Reception.Extension;
using System;
using System.Threading.Tasks;
using static Reception.App.ViewModels.IMainViewModel;

namespace Reception.App.ViewModels
{
    public abstract class BaseViewModel : ReactiveObject, IRoutableViewModel
    {
        private readonly Action _clearNotification;
        private readonly ShowErrorAction _showErrorAction;
        private readonly Action<string, NotificationType> _setNotification;

        protected BaseViewModel(string urlPathSegment, IMainViewModel mainVM)
        {
            _clearNotification = () => mainVM.ClearNotification();
            _showErrorAction = mainVM.ShowError;
            _setNotification = mainVM.SetNotification;

            UrlPathSegment = urlPathSegment;

            ThrownExceptions.Subscribe(ErrorHandler(nameof(BaseViewModel)));
        }

        public IScreen HostScreen { get; }

        [Reactive]
        public bool IsLoading { get; set; }

        public string UrlPathSegment { get; }

        public event Func<Task<bool>> Initialized;

        protected void ClearNotification()
        {
            _clearNotification.Invoke();
            IsLoading = false;
        }

        protected Action<Exception> ErrorHandler(string methodName)
        {
            return error =>
            {
                IsLoading = false;
                _showErrorAction(error, methodName);
            };
        }

        protected void OnInitialized()
        {
            Initialized?.Invoke();
            Initialized?.GetInvocationList().ForEach(d => Initialized -= (Func<Task<bool>>)d);
        }

        protected void SetNotification(string message, NotificationType type)
        {
            _setNotification?.Invoke(message, type);
            IsLoading = type == NotificationType.Refreshing;
        }
    }
}