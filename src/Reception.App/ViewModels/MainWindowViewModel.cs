using Avalonia;
using Avalonia.Logging;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reception.App.Enums;
using Reception.App.Extensions;
using Reception.App.Model.Auth;
using Reception.App.Model.PersonInfo;
using Reception.App.Network.Chat;
using Reception.App.Network.Exceptions;
#if !DEBUG
using Reception.App.Network.Server;
#endif
using Reception.App.Service.Interface;
using Reception.Constant;
using Splat;
using System;
using System.Net;
using System.Reactive;
#if !DEBUG
using System.Reactive.Linq; 
#endif
using System.Threading.Tasks;
using static Reception.App.ViewModels.IMainViewModel;

namespace Reception.App.ViewModels
{
    public class MainWindowViewModel : ReactiveObject, IMainViewModel
    {
        #region Fields

        private readonly IClientService _clientService;
#if !DEBUG
        private readonly IPingService _pingService; 
#endif
        private readonly ISettingsService _settingsService;

        #endregion

        public MainWindowViewModel()
        {
            _clientService ??= Locator.Current.GetService<IClientService>();
#if !DEBUG
            _pingService ??= Locator.Current.GetService<IPingService>(); 
#endif
            _settingsService ??= Locator.Current.GetService<ISettingsService>();

            CenterMessage = "Loading..";

            ShowError = async (error, sourceName, properties) => await ShowErrorInternal(error, sourceName, properties);

            ServerStatusMessage = ConnectionStatuses.OFFLINE.ToLower();
            StatusMessage = ConnectionStatuses.OFFLINE.ToLower();

            Router = new RoutingState();

#if !DEBUG
            _ = Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(_settingsService.PingDelay), RxApp.MainThreadScheduler)
                          .Subscribe(async x => await TryPing()); 
#else
            ServerStatusMessage = ConnectionStatuses.ONLINE.ToLower();
#endif

            CenterMessage = string.Empty;

            InitChangeThemeCommand();

            NavigateToAuth();
        }

        #region Properties

        [Reactive]
        public AuthenticateResponse AuthData { get; set; }

        public ReactiveCommand<bool, Unit> ChangeThemeCommand { get; private set; }

        [Reactive]
        public string CenterMessage { get; set; }

        [Reactive]
        public bool IsLogined { get; set; }

        [Reactive]
        public string NotificationMessage { get; set; }

        [Reactive]
        public NotificationType NotificationType { get; set; }

        public RoutingState Router { get; }

        [Reactive]
        public string ServerStatusMessage { get; set; }

        public ShowErrorAction ShowError { get; }

        [Reactive]
        public string StatusMessage { get; set; }

        #endregion

        #region Methods

        public void ClearNotification()
        {
            SetNotification(null, NotificationType.No);
        }

        public void NavigateBack(AuthenticateResponse authData)
        {
            AuthData = authData;
            LoadIsBossMode();
        }

        public void SetNotification(string message, NotificationType type)
        {
            NotificationType = type;
            NotificationMessage = message;
        }

        private void InitChangeThemeCommand()
        {
            ChangeThemeCommand = ReactiveCommand.Create<bool>(SetTheme);
        }

        private void LoadIsBossMode()
        {
            if (_settingsService.IsBoss)
            {
                Router.Navigate.Execute(new BossViewModel(this));
            }
            else
            {
                Router.Navigate.Execute(new SubordinateViewModel(this));
            }
        }

        private void NavigateToAuth()
        {
            Router.Navigate.Execute(new AuthViewModel(this));
        }

        private void SetTheme(bool isDark)
        {
            if (isDark)
            {
                GlobalCommand.UseMaterialUIDarkTheme();
            }
            else
            {
                GlobalCommand.UseMaterialUILightTheme();
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

#if !DEBUG
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
#endif

        #endregion
    }
}