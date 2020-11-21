using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reception.App.Model.Auth;
using Reception.App.Network.Auth;
using Reception.Extensions;
using Splat;
using System;
using System.Reactive;
using System.Threading.Tasks;

namespace Reception.App.ViewModels
{
    public class AuthViewModel : BaseViewModel
    {
        private readonly IUserService _userService;

        #region ctor
        public AuthViewModel(IScreen parentViewModel = null)
        {
            UrlPathSegment = nameof(AuthViewModel);

            HostScreen = parentViewModel ?? Locator.Current.GetService<IScreen>();

            _userService ??= Locator.Current.GetService<IUserService>();

            #region Init NavigateCommand
            var canNavigate = this.WhenAnyValue(x => x.AuthData, aData => aData.IsAuthInfoCorrect());
            NavigateCommand = ReactiveCommand.Create<AuthenticateResponse>(NavigateBack, canNavigate);
            this.WhenAnyValue(x => x.AuthData).InvokeCommand(NavigateCommand);
            #endregion

            #region Init LoginCommand
            var canLogin =
                this.WhenAnyValue(
                    x => x.Login, x => x.Password,
                    (login, password) => !login.IsNullOrWhiteSpace() && !password.IsNullOrWhiteSpace());
            LoginCommand = ReactiveCommand.CreateFromTask(LoginExecute, canLogin);
            LoginCommand.ThrownExceptions.Subscribe(error => MainVM.ShowError(error, nameof(LoginExecute)));
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
        private async Task<bool> AuthDataNotExpired(AuthenticateResponse loadedAuthData)
        {
            // ToDo: Delete this hack
            await Task.Delay(TimeSpan.FromSeconds(3));

            // ToDo: change to auth server token check
            return loadedAuthData.IsAuthInfoCorrect();
        }

        private async Task<bool> AuthViewModel_Initialized()
        {
            var loadedAuthData = LoadAuthData();
            if (await AuthDataNotExpired(loadedAuthData))
            {
                AuthData = loadedAuthData;
            }
            return true;
        }

        private AuthenticateResponse LoadAuthData()
        {
            // ToDo: Load from app data
            return new AuthenticateResponse { Id = 1, Token = "qwe" };
        }

        private async Task LoginExecute()
        {
            var request = new AuthenticateRequest { Password = Password, Username = Login };
            AuthData = await _userService.Authenticate(request);
        }

        private void NavigateBack(AuthenticateResponse authData)
        {
            MainVM.AuthData = authData;
            MainVM.LoadIsBossMode();
        }
        #endregion
    }
}