﻿using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reception.App.Constants;
using Reception.App.Enums;
using Reception.App.Localization;
using Reception.Extension;
using Reception.Extension.Helpers;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;

namespace Reception.App.ViewModels
{
    public abstract class BaseViewModel : ReactiveObject, IRoutableViewModel
    {
        protected readonly MainViewModel _mainViewModel;

        protected BaseViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            UrlPathSegment = CallingClass.GetName();

            ThrownExceptions.Subscribe(ErrorHandler(nameof(BaseViewModel)));

            Initialized += OnViewModelInitialized;

            var initMessageKey = GetType().Name.Replace(AppSystem.VIEW_MODEL, AppSystem.LOAD_DATA);
            SetRefreshingNotification(Localizer.Instance[initMessageKey]);
        }

        public event Func<Task> Initialized;

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
            Initialized?.GetInvocationList().ForEach(d => Initialized -= (Func<Task>)d);
        }

        protected virtual Task OnViewModelInitialized()
        {
            throw new NotImplementedException();
        }

        protected void SetNotification(string message, NotificationType type)
        {
            _mainViewModel.SetNotification(message, type);
            IsLoading = type == NotificationType.Refreshing;
        }

        protected void SetNotImplementedMessage<T>(T value, [CallerMemberName] string methodName = null)
        {
            _mainViewModel.ShowError(new NotImplementedException($"{methodName} not implemented"), properties: value);
        }

        protected void SetRefreshingNotification(string message)
        {
            SetNotification(message, NotificationType.Refreshing);
        }

        #endregion
    }
}
