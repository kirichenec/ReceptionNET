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
        private readonly ISettingsService _settings;

        public UserService()
        {
            _settings = Locator.Current.GetService<ISettingsService>();
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
        {
            var response = await Core.ExecutePostTaskAsync($"{_settings.UserServerPath}/User/Authenticate", request);

            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<AuthenticateResponse>(response.Content);
                return content;
            }

            throw new QueryException(response.StatusDescription);
        }
    }
}
