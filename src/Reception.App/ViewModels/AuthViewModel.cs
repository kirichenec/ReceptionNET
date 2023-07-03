using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reception.App.Enums;
using Reception.App.Localization;
using Reception.App.Model.Auth;
using Reception.App.Network.Auth;
using Reception.App.Network.Exceptions;
using Reception.Extension;
using System.Net;
using System.Reactive;
using System.Reactive.Linq;

namespace Reception.App.ViewModels
{
    public class AuthViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;

        public AuthViewModel(IAuthService authService, MainViewModel mainViewModel)
            : base(mainViewModel)
        {
            _authService = authService;

            InitApplyAuthCommand();
            InitLoginCommand();

            OnInitialized();
        }

        #region Properties

        public ReactiveCommand<AuthenticateResponse, Unit> ApplyAuthCommand { get; private set; }

        [Reactive]
        public AuthenticateResponse AuthData { get; set; }

        [Reactive]
        public string Login { get; set; }

        public ReactiveCommand<Unit, Unit> LoginCommand { get; private set; }

        [Reactive]
        public string Password { get; set; }

        #endregion

        #region Methods

        protected override async Task OnViewModelInitialized()
        {
            try
            {
                SetLoadingState(true);
                if (await _authService.IsAuthValid())
                {
                    AuthData = _authService.AuthData;
                }
                SetLoadingState(false);
            }
            catch (QueryException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                // it's normal here
                SetLoadingState(false);
            }
            catch (Exception ex)
            {
                ErrorHandler(nameof(OnViewModelInitialized)).Invoke(ex);
            }
        }

        private void InitApplyAuthCommand()
        {
            var canNavigate = this.WhenAnyValue(
                x => x.AuthData,
                selector: aData => aData.IsAuthInfoCorrect());

            ApplyAuthCommand = ReactiveCommand.Create<AuthenticateResponse>(_mainViewModel.ApplyAuthData, canNavigate);
            this.WhenAnyValue(x => x.AuthData)
                .InvokeCommand(ApplyAuthCommand);
            ApplyAuthCommand.ThrownExceptions.Subscribe(ErrorHandler(nameof(ApplyAuthCommand)));
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

        private async Task LoginExecuteAsync()
        {
            SetLoadingState(true);
            AuthData = await _authService.Authenticate(Login, Password);
            SetLoadingState(false);
        }

        private void SetLoadingState(bool state)
        {
            var message = string.Empty;
            var notificationType = NotificationType.No;
            if (state)
            {
                message = Localizer.Instance["AuthChecking"];
                notificationType = NotificationType.Refreshing;
            }
            SetNotification(message, notificationType);
        }

        #endregion
    }
}
