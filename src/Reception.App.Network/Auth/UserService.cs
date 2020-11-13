using Newtonsoft.Json;
using Reception.App.Model.Auth;
using Reception.App.Network.Exceptions;
using System.Threading.Tasks;

namespace Reception.App.Network.Auth
{
    public class UserService : IUserService
    {
        private readonly string _serverPath;

        public UserService(string serverPath)
        {
            _serverPath = serverPath;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
        {
            var response = await Core.ExecutePostTaskAsync($"{_serverPath}/User/Authenticate", request);

            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<AuthenticateResponse>(response.Content);
                return content;
            }

            throw new QueryException(response.StatusDescription);
        }
    }
}
