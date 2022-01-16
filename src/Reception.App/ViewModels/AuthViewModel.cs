using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reception.App.Model.Auth;
using Reception.App.Network.Auth;
using Reception.Extension;
using Splat;
using System;
using System.Reactive;
using System.Threading.Tasks;

namespace Reception.App.ViewModels
{
    public class AuthViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;

        #region ctor

        public AuthViewModel(IMainViewModel mainWindowViewModel) : base(nameof(AuthViewModel), mainWindowViewModel.ShowError)
        {
            _authService ??= Locator.Current.GetService<IAuthService>();

            #region Init NavigateCommand
            var canNavigate = this.WhenAnyValue(x => x.AuthData, aData => aData.IsAuthInfoCorrect());
            NavigateCommand = ReactiveCommand.Create<AuthenticateResponse>(mainWindowViewModel.NavigateBack, canNavigate);
            this.WhenAnyValue(x => x.AuthData).InvokeCommand(NavigateCommand);
            #endregion

            #region Init LoginCommand
            var canLogin =
                this.WhenAnyValue(
                    x => x.Login, x => x.Password,
                    (login, password) => !login.IsNullOrWhiteSpace() && !password.IsNullOrWhiteSpace());
            LoginCommand = ReactiveCommand.CreateFromTask(LoginExecute, canLogin);
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
            if (await _authService.IsAuthValid())
            {
                AuthData = _authService.AuthData;
                return true;
            }
            return false;
        }

        private async Task LoginExecute()
        {
            AuthData = await _authService.Authenticate(Login, Password);
        }
        #endregion
    }
}