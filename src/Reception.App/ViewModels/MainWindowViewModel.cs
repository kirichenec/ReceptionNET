using Avalonia.Logging;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reception.App.Constants;
using Reception.App.Extensions;
using Reception.App.Model.Auth;
using Reception.App.Model.PersonInfo;
using Reception.App.Models;
using Reception.App.Network.Exceptions;
using Reception.App.Network.Server;
using Splat;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Reception.App.ViewModels
{
    public class MainWindowViewModel : ReactiveObject, IScreen
    {
        #region Fields
        private readonly IPingService _pingService;
        #endregion

        #region ctor
        public MainWindowViewModel()
        {
            ViewLocator.MainVM = this;

            AuthData = LoadAuthData();

            CenterMessage = "Loading..";

            ServerStatusMessage = ConnectionStatus.OFFLINE.ToLower();
            StatusMessage = ConnectionStatus.OFFLINE.ToLower();

            Router = new RoutingState();

            _pingService ??= Locator.Current.GetService<IPingService>();

            Observable
                .Timer(TimeSpan.Zero, TimeSpan.FromSeconds(AppSettings.PingDelay), RxApp.MainThreadScheduler)
                .Subscribe(async x => await TryPing());

            //Router.CurrentViewModel.Subscribe(x =>
            //{
            //    if (!(Router.GetCurrentViewModel() is IRoutableViewModel) && AuthData.Token != null)
            //    {
                    LoadIsBossMode();
            //    }
            //});

            //Router.Navigate.Subscribe(x =>
            //{
            //    if (Router.GetCurrentViewModel() is MainWindowViewModel)
            //    {
            //        LoadIsBossMode();
            //    }
            //});

            //NavigateToAuth();
        }
        #endregion

        #region Enums
        public enum ErrorType
        {
            No,
            Server,
            Connection,
            System,
            Request
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

        [Reactive]
        public string StatusMessage { get; set; }
        #endregion

        #region Methods
        private AuthenticateResponse LoadAuthData()
        {
            return new AuthenticateResponse();
        }

        private void LoadIsBossMode()
        {
            if (AppSettings.IsBoss)
            {
                Router.Navigate.Execute(new BossViewModel(this));
            }
            else
            {
                Router.Navigate.Execute(new SubordinateViewModel(this));
            }
            CenterMessage = string.Empty;
        }

        private void NavigateToAuth()
        {
            Router.Navigate.Execute(new AuthViewModel(this));
        }

        [SuppressMessage("Major Bug", "S3343:Caller information parameters should come at the end of the parameter list", Justification = "<Pending>")]
        public void ShowError(Exception error, [CallerMemberName] string name = null, params object[] properties)
        {
            Logger.Sink.LogException(name, this, typeof(Exception), properties);
            ErrorMessage = error.Message;
            if (error is NotFoundException<Person>)
            {
                LastErrorType = ErrorType.Request;
                return;
            }
            if (error is QueryException)
            {
                LastErrorType = ErrorType.Server;
                return;
            }
            LastErrorType = ErrorType.System;
        }

        private async Task TryPing()
        {
            try
            {
                await _pingService.PingAsync();
                ServerStatusMessage = ConnectionStatus.ONLINE.ToLower();
                if (LastErrorType == ErrorType.Server)
                {
                    LastErrorType = ErrorType.No;
                    ErrorMessage = null;
                }
            }
            catch (Exception ex)
            {
                ServerStatusMessage = ConnectionStatus.OFFLINE.ToLower();
                LastErrorType = ErrorType.Server;
                ErrorMessage = ex.Message;
            }
        }
        #endregion
    }
}