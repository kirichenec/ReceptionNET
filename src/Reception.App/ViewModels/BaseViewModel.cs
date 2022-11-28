using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reception.App.Enums;
using Reception.Extension;
using Reception.Extension.Helpers;
using Splat;

namespace Reception.App.ViewModels
{
    public abstract class BaseViewModel : ReactiveObject, IRoutableViewModel
    {
        protected readonly IMainViewModel _mainViewModel;

        protected BaseViewModel()
        {
            _mainViewModel = Locator.Current.GetService<IViewFor<MainWindowViewModel>>().ViewModel;

            UrlPathSegment = CallingClass.GetName();

            ThrownExceptions.Subscribe(ErrorHandler(nameof(BaseViewModel)));
        }

        public event Func<Task<bool>> Initialized;

        #region Properties

        public IScreen HostScreen { get; }

        [Reactive]
        public bool IsLoading { get; set; }

        public string UrlPathSegment { get; }

        #endregion

        #region Methods

        protected void ClearNotification()
        {
            _mainViewModel.ClearNotification();
            IsLoading = false;
        }

        protected Action<Exception> ErrorHandler(string methodName)
        {
            return error =>
            {
                IsLoading = false;
                _mainViewModel.ShowError(error, methodName);
            };
        }

        protected void OnInitialized()
        {
            Initialized?.Invoke();
            Initialized?.GetInvocationList().ForEach(d => Initialized -= (Func<Task<bool>>)d);
        }

        protected void SetNotification(string message, NotificationType type)
        {
            _mainViewModel.SetNotification(message, type);
            IsLoading = type == NotificationType.Refreshing;
        }

        protected void SetRefreshingNotification(string message)
        {
            SetNotification(message, NotificationType.Refreshing);
        }

        #endregion
    }
}