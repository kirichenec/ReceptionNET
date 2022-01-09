using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reception.App.Model.Auth;
using Reception.App.Network.Auth;
using Reception.App.Service.Interface;
using Reception.Extension;
using Splat;
using System;
using System.Reactive;
using System.Threading.Tasks;

namespace Reception.App.ViewModels
{
    public class AuthViewModel : BaseViewModel
    {
        private readonly IMainViewModel _mainWindowViewModel;
        private readonly IUserService _userService;
        private readonly ISettingsService _settingsService;

        #region ctor

        public AuthViewModel(IMainViewModel mainWindowViewModel) : base(nameof(AuthViewModel), mainWindowViewModel.ShowError)
        {
            _mainWindowViewModel = mainWindowViewModel;

            _settingsService ??= Locator.Current.GetService<ISettingsService>();
            _userService ??= Locator.Current.GetService<IUserService>();

            #region Init NavigateCommand
            var canNavigate = this.WhenAnyValue(x => x.AuthData, aData => aData.IsAuthInfoCorrect());
            NavigateCommand = ReactiveCommand.Create<AuthenticateResponse>(_mainWindowViewModel.NavigateBack, canNavigate);
            this.WhenAnyValue(x => x.AuthData).InvokeCommand(NavigateCommand);
            #endregion

            #region Init LoginCommand
            var canLogin =
                this.WhenAnyValue(
                    x => x.Login, x => x.Password,
                    (login, password) => !login.IsNullOrWhiteSpace() && !password.IsNullOrWhiteSpace());
            LoginCommand = ReactiveCommand.CreateFromTask(LoginExecute, canLogin);
            LoginCommand.ThrownExceptions.Subscribe(error => _mainWindowViewModel.ShowError(error, nameof(LoginExecute)));
            #endregion

            Initialized += AuthViewModel_Initialized;
            OnInitialized();
        }

        #endregion

        #region Properties
        [Reactive]
        public AuthenticateResponse AuthData { get; set; }

        [Reactive]
        public string Login { get; set; }

        [Reactive]
        public string Password { get; set; }
        #endregion

        #region Commands
        public ReactiveCommand<Unit, Unit> LoginCommand { get; }

        public ReactiveCommand<AuthenticateResponse, Unit> NavigateCommand { get; set; }
        #endregion

        #region Methods
        private async Task<bool> AuthViewModel_Initialized()
        {
            LoadAuthData();
            if (await _userService.IsAuthValid())
            {
                AuthData = _userService.AuthData;
                return true;
            }
            return false;
        }

        private void LoadAuthData()
        {
            var authInfo = new AuthenticateResponse
            {
                Id = _settingsService.Token.UserId,
                Token = _settingsService.Token.Value
            };

            _userService.SetUserAuth(authInfo.Id, authInfo.Token);
        }

        private async Task LoginExecute()
        {
            var request = new AuthenticateRequest { Password = Password, Login = Login };
            AuthData = await _userService.Authenticate(request);
            _settingsService.Token = new Token { UserId = AuthData.Id, Value = AuthData.Token };
        }
        #endregion
    }
}