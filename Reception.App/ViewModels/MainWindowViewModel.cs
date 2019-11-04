using ReactiveUI;
using Reception.App.Models;
using Reception.App.Network;
using Splat;
using System;
using System.Reactive;

namespace Reception.App.ViewModels
{
    public class MainWindowViewModel : ReactiveObject, IScreen
    {
        #region Fields
        private readonly INetworkService<Person> _networkServiceOfPersons;

        private readonly IPingService _pingService;

        private string _errorMessage;
        #endregion

        #region ctor
        public MainWindowViewModel()
        {
            _networkServiceOfPersons = _networkServiceOfPersons ?? Locator.Current.GetService<INetworkService<Person>>();
            _pingService = _pingService ?? Locator.Current.GetService<IPingService>();

            ClearErrorMessageCommand = ReactiveCommand.Create(ClearErrorMessage);

            LoadIsBossMode();

            // ping for up server
            var pingAnswer = _pingService.PingAsync();
        }
        #endregion

        #region Properties
        public RoutingState Router { get; } = new RoutingState();

        public string ErrorMessage { get => _errorMessage; set => this.RaiseAndSetIfChanged(ref _errorMessage, value); }
        #endregion

        #region Commands
        public ReactiveCommand<Unit, Unit> ClearErrorMessageCommand { get; set; }
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

        private void ClearErrorMessage()
        {
            ErrorMessage = null;
        }
        #endregion
    }
}
