using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reception.App.Enums;
using Reception.App.Model.Auth;
using Reception.App.Network.Auth;
using Reception.App.Network.Exceptions;
using Reception.Extension;
using Splat;
using System;
using System.Net;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Reception.App.ViewModels
{
    public class AuthViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;

        private readonly IMainViewModel _mainViewModel;

        #region ctor

        public AuthViewModel(IMainViewModel mainViewModel) : base(nameof(AuthViewModel), mainViewModel)
        {
            SetRefreshingNotification("Loading auth data");

            _mainViewModel = mainViewModel;

            _authService ??= Locator.Current.GetService<IAuthService>();

            InitNavigateCommand();
            InitLoginCommand();

            Initialized += OnAuthViewModelInitialized;
            OnInitialized();
        }

        private void InitLoginCommand()
        {
            var canLogin = this.WhenAnyValue(
                x => x.Login,
                x => x.Password,
                selector: (login, password) => !login.IsNullOrWhiteSpace() && !password.IsNullOrWhiteSpace());

            LoginCommand = ReactiveCommand.CreateFromTask(LoginExecuteAsync, canLogin);
            LoginCommand.ThrownExceptions.Subscribe(ErrorHandler(nameof(LoginCommand)));
        }

        private void InitNavigateCommand()
        {
            var canNavigate = this.WhenAnyValue(
                x => x.AuthData,
                selector: aData => aData.IsAuthInfoCorrect());

            NavigateCommand = ReactiveCommand.Create<AuthenticateResponse>(_mainViewModel.NavigateBack, canNavigate);
            this.WhenAnyValue(x => x.AuthData)
                .InvokeCommand(NavigateCommand);
            NavigateCommand.ThrownExceptions.Subscribe(ErrorHandler(nameof(NavigateCommand)));
        }

        #endregion

        #region Properties

        [Reactive]
        public AuthenticateResponse AuthData { get; set; }

        [Reactive]
        public string Login { get; set; }

        public ReactiveCommand<Unit, Unit> LoginCommand { get; private set; }

        public ReactiveCommand<AuthenticateResponse, Unit> NavigateCommand { get; private set; }

        [Reactive]
        public string Password { get; set; }

        #endregion

        #region Methods

        private async Task<bool> OnAuthViewModelInitialized()
        {
            SetLoadingState(true);
            var isAuthValid = false;
            try
            {
                isAuthValid = await _authService.IsAuthValid();
                if (isAuthValid)
                {
                    AuthData = _authService.AuthData;
                }
            }
            catch (QueryException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                // it's normal here
            }
            catch (Exception ex)
            {
                ErrorHandler(nameof(OnAuthViewModelInitialized)).Invoke(ex);
            }

            SetLoadingState(false);
            return isAuthValid;
        }

        private void SetLoadingState(bool state)
        {
            var message = string.Empty;
            var notificationType = NotificationType.No;
            if (state)
            {
                message = "Auth checking. Please, wait..";
                notificationType = NotificationType.Refreshing;
            }
            SetNotification(message, notificationType);
        }

        private async Task LoginExecuteAsync()
        {
            SetLoadingState(true);
            AuthData = await _authService.Authenticate(Login, Password);
            SetLoadingState(false);
        }

        #endregion
    }
}