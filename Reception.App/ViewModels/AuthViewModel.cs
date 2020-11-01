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
        public AuthViewModel(IScreen parentViewModel)
        {
            UrlPathSegment = nameof(AuthViewModel);
            HostScreen = parentViewModel;

            _userService ??= Locator.Current.GetService<IUserService>();

            #region Init LoginCommand
            var canLogin = this.WhenAnyValue(x => x.Login, login => !login.IsNullOrWhiteSpace());
            LoginCommand = ReactiveCommand.CreateFromTask<Unit, AuthenticateResponse>(LoginExecute, canLogin);
            LoginCommand.ThrownExceptions.Subscribe(error => ViewLocator.MainVM.ShowError(error, nameof(LoginExecute)));
            LoginCommand.Subscribe(aData => ViewLocator.MainVM.AuthData = aData);
            #endregion

            #region Init NavigateCommand
            //var canNavigate = this.WhenAnyValue(x => ViewLocator.MainVM.AuthData, aData => aData.IsAuthInfoCorrect());
            //NavigateCommand = ReactiveCommand.Create<AuthenticateResponse>(Navigate, canNavigate);
            //this.WhenAnyValue(x => ViewLocator.MainVM.AuthData).InvokeCommand(NavigateCommand);
            #endregion
        }
        #endregion

        #region Properties
        [Reactive]
        public string Login { get; set; }

        [Reactive]
        public string Password { get; set; }
        #endregion

        #region Commands
        public ReactiveCommand<Unit, AuthenticateResponse> LoginCommand { get; }

        public ReactiveCommand<AuthenticateResponse, Unit> NavigateCommand { get; set; }
        #endregion

        #region Methods
        private async Task<AuthenticateResponse> LoginExecute(Unit _)
        {
            var request = new AuthenticateRequest { Password = Password, Username = Login };
            var responce = await _userService.Authenticate(request);
            return responce;
        }

        private void Navigate(AuthenticateResponse authData)
        {
            ViewLocator.MainVM.Router.NavigateBack.Execute();
        }
        #endregion
    }
}