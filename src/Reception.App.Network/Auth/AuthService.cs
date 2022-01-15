using Newtonsoft.Json;
using Reception.App.Model.Auth;
using Reception.App.Network.Exceptions;
using Reception.App.Service.Interface;
using Reception.Constant;
using Splat;
using System.Threading.Tasks;

namespace Reception.App.Network.Auth
{
    public class AuthService : IAuthService
    {
        private AuthenticateResponse _authData = new();

        private readonly ISettingsService _settingsService;

        public AuthService()
        {
            _settingsService = Locator.Current.GetService<ISettingsService>();
        }

        public AuthenticateResponse AuthData => _authData;

        public string UserRootUri => $"{_settingsService.AuthServerPath}/User";

        public async Task<AuthenticateResponse> Authenticate(string login, string password)
        {
            var request = new AuthenticateRequest { Login = login, Password = password };

            var response = await Core.ExecutePostTaskAsync(
                baseUrl: UserRootUri,
                methodUri: "Authenticate",
                bodyValue: request);

            if (response.IsSuccessful)
            {
                _authData = JsonConvert.DeserializeObject<AuthenticateResponse>(response.Content);
                _settingsService.Token = new Token { UserId = _authData.Id, Value = _authData.Token };
                return _authData;
            }

            throw new QueryException(response.StatusDescription, response.StatusCode);
        }

        public (string, string)[] GetDefaultHeaders() => new[] { (HttpHeaders.TOKEN, _authData.Token) };

        public async Task<bool> IsAuthValid()
        {
            UpdateUserAuth();

            var response = await Core.ExecuteGetTaskAsync(
                baseUrl: UserRootUri,
                methodUri: $"IsAuthValid",
                headers: GetDefaultHeaders());

            if (response.IsSuccessful)
            {
                return true;
            }

            throw new QueryException(response.StatusDescription, response.StatusCode);


            void UpdateUserAuth()
            {
                _authData.Id = _settingsService.Token.UserId;
                _authData.Token = _settingsService.Token.Value;
            }
        }
    }
}