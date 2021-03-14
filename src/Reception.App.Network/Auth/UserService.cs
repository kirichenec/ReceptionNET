using Newtonsoft.Json;
using Reception.App.Model.Auth;
using Reception.App.Network.Exceptions;
using Reception.App.Service.Interface;
using Splat;
using System.Threading.Tasks;

namespace Reception.App.Network.Auth
{
    public class UserService : IUserService
    {
        private AuthenticateResponse _authData;

        private readonly ISettingsService _settings;

        public UserService()
        {
            _settings = Locator.Current.GetService<ISettingsService>();
        }

        public AuthenticateResponse AuthData => _authData;

        public string UserRootUri => $"{_settings.UserServerPath}/User";

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
        {
            var response = await Core.ExecutePostTaskAsync($"{UserRootUri}/authenticate", request);

            if (response.IsSuccessful)
            {
                _authData = JsonConvert.DeserializeObject<AuthenticateResponse>(response.Content);
                return _authData;
            }

            throw new QueryException(response.StatusDescription);
        }

        public async Task<bool> IsAuthValid()
        {
            var response = await Core.ExecuteGetTaskAsync($"{UserRootUri}/IsAuthValid");


            if (response.IsSuccessful)
            {
                return true;
            }

            throw new QueryException(response.StatusDescription);
        }

        public void SetUserAuth(int userId, string token)
        {
            _authData = new AuthenticateResponse { Id = userId, Token = token };
        }
    }
}