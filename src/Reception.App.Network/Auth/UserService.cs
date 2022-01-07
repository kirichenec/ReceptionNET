using Newtonsoft.Json;
using Reception.App.Model.Auth;
using Reception.App.Network.Exceptions;
using Reception.App.Service.Interface;
using Reception.Extension.Converters;
using Reception.Model.Interface;
using Splat;
using System.Threading.Tasks;

namespace Reception.App.Network.Auth
{
    public class UserService : IUserService
    {
        private AuthenticateResponse _authData = new AuthenticateResponse();

        private readonly ISettingsService _settings;

        public UserService()
        {
            _settings = Locator.Current.GetService<ISettingsService>();
        }

        public AuthenticateResponse AuthData => _authData;

        public string UserRootUri => $"{_settings.UserServerPath}/User";

        public IToken Token => new Token { UserId = _authData.Id, Value = _authData.Token };

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
        {
            var response = await Core.ExecutePostTaskAsync(
                baseUrl: UserRootUri,
                methodUri: "authenticate",
                bodyValue: request);

            if (response.IsSuccessful)
            {
                _authData = JsonConvert.DeserializeObject<AuthenticateResponse>(response.Content);
                return AuthData;
            }

            throw new QueryException(response.StatusDescription);
        }

        public async Task<bool> IsAuthValid()
        {
            var response = await Core.ExecuteGetTaskAsync(
                baseUrl: UserRootUri,
                methodUri: $"IsAuthValid",
                headers: new (string, string)[] { (nameof(IUserService.Token),Token.ToJsonString()) });

            if (response.IsSuccessful)
            {
                return true;
            }

            throw new QueryException(response.StatusDescription);
        }

        public void SetUserAuth(int userId, string token)
        {
            _authData.Id = userId;
            _authData.Token = token;
        }
    }
}