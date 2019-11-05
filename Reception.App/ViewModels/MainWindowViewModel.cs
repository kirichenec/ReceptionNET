using ReactiveUI;
using Reception.App.Constants;
using Reception.App.Models;
using Reception.App.Network;
using Splat;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Reception.App.ViewModels
{
    public class MainWindowViewModel : ReactiveObject, IScreen
    {
        #region Fields
        private string _centerMessage;

        private string _errorMessage;

        private ErrorType _lastErrorType = ErrorType.No;

        private readonly INetworkService<Person> _networkServiceOfPersons;

        private readonly IPingService _pingService;

        private string _serverStatusMessage = ConnectionStatus.OFFLINE.ToLower();

        private string _statusMessage = ConnectionStatus.OFFLINE.ToLower();
        #endregion

        #region ctor
        public MainWindowViewModel()
        {
            CenterMessage = "Loading..";

            _networkServiceOfPersons = _networkServiceOfPersons ?? Locator.Current.GetService<INetworkService<Person>>();
            _pingService = _pingService ?? Locator.Current.GetService<IPingService>();

            Observable
                .Timer(TimeSpan.Zero, TimeSpan.FromSeconds(AppSettings.PingDelay), RxApp.MainThreadScheduler)
                .Subscribe(async x => await TryPing());

            LoadIsBossMode();

            CenterMessage = "";
        }
        #endregion

        #region Enums
        enum ErrorType
        {
            No,
            Server,
            Connection
        }
        #endregion

        #region Properties
        public string CenterMessage { get => _centerMessage; set => this.RaiseAndSetIfChanged(ref _centerMessage, value); }

        public string ErrorMessage { get => _errorMessage; set => this.RaiseAndSetIfChanged(ref _errorMessage, value); }

        public RoutingState Router { get; } = new RoutingState();

        public string ServerStatusMessage { get => _serverStatusMessage; set => this.RaiseAndSetIfChanged(ref _serverStatusMessage, value); }

        public string StatusMessage { get => _statusMessage; set => this.RaiseAndSetIfChanged(ref _statusMessage, value); }
        #endregion

        #region Methods
        private void LoadIsBossMode()
        {
            try
            {
                if (AppSettings.IsBoss)
                {
                    Router.Navigate.Execute(new BossViewModel(this));
                }
                else
                {
                    Router.Navigate.Execute(new SubordinateViewModel(this, _networkServiceOfPersons));
                }
            }
            catch (Exception)
            {
                ErrorMessage = "Can't load IsBoss mode";
            }
        }

        private async Task TryPing()
        {
            try
            {
                await _pingService.PingAsync();
                ServerStatusMessage = ConnectionStatus.ONLINE.ToLower();
                if (_lastErrorType == ErrorType.Server)
                {
                    _lastErrorType = ErrorType.No;
                    ErrorMessage = null;
                }
            }
            catch (Exception ex)
            {
                ServerStatusMessage = ConnectionStatus.OFFLINE.ToLower();
                _lastErrorType = ErrorType.Server;
                ErrorMessage = ex.Message;
            }
        }
        #endregion
    }
}