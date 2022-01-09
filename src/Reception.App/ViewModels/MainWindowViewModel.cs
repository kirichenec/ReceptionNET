using Avalonia.Logging;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reception.App.Constants;
using Reception.App.Extensions;
using Reception.App.Model.Auth;
using Reception.App.Model.PersonInfo;
using Reception.App.Network.Chat;
using Reception.App.Network.Exceptions;
using Reception.App.Network.Server;
using Reception.App.Service.Interface;
using Reception.App.ViewModels.Enums;
using Splat;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using static Reception.App.ViewModels.IMainViewModel;

namespace Reception.App.ViewModels
{
    public class MainWindowViewModel : ReactiveObject, IMainViewModel
    {
        #region Fields

        private readonly IClientService _clientService;
        private readonly IPingService _pingService;
        private readonly ISettingsService _settingsService;

        #endregion

        #region ctor

        public MainWindowViewModel()
        {
            _clientService ??= Locator.Current.GetService<IClientService>();
            _pingService ??= Locator.Current.GetService<IPingService>();
            _settingsService ??= Locator.Current.GetService<ISettingsService>();

            CenterMessage = "Loading..";

            ShowError = async (error, sourceName, properties) => await ShowErrorInternal(error, sourceName, properties);

            ServerStatusMessage = ConnectionStatus.OFFLINE.ToLower();
            StatusMessage = ConnectionStatus.OFFLINE.ToLower();

            Router = new RoutingState();

            _ = Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(_settingsService.PingDelay), RxApp.MainThreadScheduler)
                          .Subscribe(async x => await TryPing());

            CenterMessage = string.Empty;

            NavigateToAuth();
        }

        #endregion

        #region Properties

        [Reactive]
        public AuthenticateResponse AuthData { get; set; }

        [Reactive]
        public string CenterMessage { get; set; }

        [Reactive]
        public string ErrorMessage { get; set; }

        [Reactive]
        public bool IsLogined { get; set; }

        [Reactive]
        public ErrorType LastErrorType { get; set; }

        public RoutingState Router { get; }

        [Reactive]
        public string ServerStatusMessage { get; set; }

        public ShowErrorAction ShowError { get; }

        [Reactive]
        public string StatusMessage { get; set; }

        #endregion

        #region Methods

        public void ClearErrorInfo()
        {
            SetErrorInfo(null, ErrorType.No);
        }

        public void NavigateBack(AuthenticateResponse authData)
        {
            AuthData = authData;
            LoadIsBossMode();
        }

        public void SetErrorInfo(string message, ErrorType type)
        {
            LastErrorType = type;
            ErrorMessage = message;
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

        [SuppressMessage("Major Bug", "S3343:Caller information parameters should come at the end of the parameter list", Justification = "<Pending>")]
        private async Task ShowErrorInternal(Exception error, [CallerMemberName] string sourceName = null, params object[] properties)
        {
            Logger.Sink.LogException(sourceName, this, typeof(Exception), properties);

            var errorType = ErrorType.System;
            if (error is NotFoundException<Person>)
            {
                errorType = ErrorType.Request;
            }
            else if (error is QueryException queryError)
            {
                if (queryError.StatusCode == HttpStatusCode.Unauthorized)
                {
                    NavigateToAuth();
                    await _clientService.StopClientAsync();
                    return;
                }
                errorType = ErrorType.Server;
            }
            SetErrorInfo($"{sourceName}: {error.Message}", errorType);
        }

        private async Task TryPing()
        {
            try
            {
                await _pingService.PingAsync();
                ServerStatusMessage = ConnectionStatus.ONLINE.ToLower();
                if (LastErrorType == ErrorType.Server)
                {
                    ClearErrorInfo();
                }
            }
            catch (Exception ex)
            {
                ServerStatusMessage = ConnectionStatus.OFFLINE.ToLower();
                SetErrorInfo(ex.Message, ErrorType.Server);
            }
        }

        #endregion
    }
}