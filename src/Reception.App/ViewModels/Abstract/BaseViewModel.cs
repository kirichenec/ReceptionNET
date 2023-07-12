using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reception.App.Enums;
using Reception.App.Localization;
using Reception.Extension;
using System.Reactive.Linq;

namespace Reception.App.ViewModels.Abstract
{
    public abstract class BaseViewModel : ReactiveObject, IRoutableViewModel
    {
        protected readonly MainViewModel _mainViewModel;


        protected BaseViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            UrlPathSegment = GetType().Name;

            ThrownExceptions.Subscribe(ErrorHandler(nameof(BaseViewModel)));

            Initialized += OnViewModelInitialized;

            var initMessageKey = GetType().Name.Replace("ViewModel", "LoadData");
            SetRefreshingNotification(Localizer.Instance[initMessageKey]);
        }


        public event Func<Task> Initialized;


        public IScreen HostScreen { get; }

        [Reactive]
        public bool IsLoading { get; set; }

        public string UrlPathSegment { get; }


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
            Initialized?.GetInvocationList().ForEach(d => Initialized -= (Func<Task>)d);
        }

        protected abstract Task OnViewModelInitialized();

        protected void SetNotification(string message, NotificationType type)
        {
            _mainViewModel.SetNotification(message, type);
            IsLoading = type == NotificationType.Refreshing;
        }

        protected void SetRefreshingNotification(string message)
        {
            SetNotification(message, NotificationType.Refreshing);
        }
    }
}
