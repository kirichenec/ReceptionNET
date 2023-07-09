using Avalonia.Logging;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reception.App.Enums;
using Reception.App.Extensions;
using Reception.App.Localization;
using Reception.App.Model.Auth;
using Reception.App.Model.PersonInfo;
using Reception.App.Network.Chat;
using Reception.App.Network.Exceptions;
using Reception.App.Network.Server;
using Reception.App.Service.Interface;
using Reception.App.ViewModels.Abstract;
using Reception.Constant;
using Splat;
using System.Net;
using System.Reactive;
using System.Reactive.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Reception.App.ViewModels
{
    public class MainViewModel : ReactiveObject, IScreen
    {
        private readonly IClientService _clientService;
        private readonly IPingService _pingService;
        private readonly ISettingsService _settingsService;


        public MainViewModel(IClientService clientService, IPingService pingService, ISettingsService settingsService)
        {
            _clientService = clientService;
            _pingService = pingService;
            _settingsService = settingsService;

            CenterMessage = Localizer.Instance["MainLoading"];

            ShowError = async (error, sourceName, properties) => await ShowErrorInternal(error, sourceName, properties);

            ServerStatusMessage = ConnectionStatuses.OFFLINE.ToLower();
            StatusMessage = ConnectionStatuses.OFFLINE.ToLower();

            Router = new RoutingState();

            _ = Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(_settingsService.PingDelay), RxApp.MainThreadScheduler)
                          .Subscribe(async x => await TryPing());

            Settings = new SettingsViewModel(_settingsService, NavigateToAuth);

            InitCommand = ReactiveCommand.Create(NavigateToAuth);

            AppVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            CenterMessage = string.Empty;
        }


        public delegate void ShowErrorAction(Exception error, [CallerMemberName] string sourceName = null, params object[] properties);


        public string AppVersion { get; }

        [Reactive]
        public AuthenticateResponse AuthData { get; set; }

        [Reactive]
        public string CenterMessage { get; set; }

        public ReactiveCommand<Unit, Unit> InitCommand { get; }

        [Reactive]
        public bool IsLogined { get; set; }

        [Reactive]
        public string NotificationMessage { get; set; }

        [Reactive]
        public NotificationType NotificationType { get; set; }

        public RoutingState Router { get; }

        [Reactive]
        public string ServerStatusMessage { get; set; }

        [Reactive]
        public SettingsViewModel Settings { get; set; }

        public ShowErrorAction ShowError { get; }

        [Reactive]
        public string StatusMessage { get; set; }


        public void ApplyAuthData(AuthenticateResponse authData)
        {
            AuthData = authData;
            NavigateToUserInterface();
        }

        public void ClearNotification()
        {
            SetNotification(null, NotificationType.No);
        }

        public void SetNotification(string message, NotificationType type)
        {
            NotificationType = type;
            NotificationMessage = message;
        }

        private void NavigateTo<T>() where T : BaseViewModel
        {
            if (Router.NavigationStack.LastOrDefault() is not T)
            {
                Router.NavigationStack.Clear();
                Router.Navigate.Execute(Locator.Current.GetService<T>());
            }
        }

        private void NavigateToAuth()
        {
            NavigateTo<AuthViewModel>();
        }

        private void NavigateToUserInterface()
        {
            if (_settingsService.IsBoss)
            {
                NavigateTo<BossViewModel>();
            }
            else
            {
                NavigateTo<SubordinateViewModel>();
            }
        }

        private async Task ShowErrorInternal(Exception error, string sourceName, params object[] properties)
        {
            Logger.Sink.LogException(sourceName, this, typeof(Exception), properties);

            var errorType = NotificationType.System;
            switch (error)
            {
                case NotFoundException<Person>:
                    errorType = NotificationType.Request;
                    break;
                case QueryException unauthorizedError when unauthorizedError.StatusCode == HttpStatusCode.Unauthorized:
                    await _clientService.StopClientAsync();
                    NavigateToAuth();
                    return;
                case QueryException:
                    errorType = NotificationType.Server;
                    break;
                case OperationCanceledException:
                    return;
                default:
                    break;
            }
            SetNotification($"{sourceName}: {error.Message}", errorType);
        }

        private async Task TryPing()
        {
            try
            {
                await _pingService.PingAsync();
                ServerStatusMessage = ConnectionStatuses.ONLINE.ToLower();
                if (NotificationType == NotificationType.Server)
                {
                    ClearNotification();
                }
            }
            catch (Exception ex)
            {
                ServerStatusMessage = ConnectionStatuses.OFFLINE.ToLower();
                SetNotification(ex.Message, NotificationType.Server);
            }
        }
    }
}