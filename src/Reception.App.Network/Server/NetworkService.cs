using Newtonsoft.Json;
using Reception.App.Network.Auth;
using Reception.App.Network.Exceptions;
using Reception.Extension.Converters;
using Reception.Model.Interface;
using Reception.Model.Network;
using Splat;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reception.App.Network.Server
{
    public class NetworkService<T> : INetworkService<T>
    {
        private readonly IUserService _userService;
        private readonly string _serverPath;

        public NetworkService(string serverPath)
        {
            _userService = Locator.Current.GetService<IUserService>();
            _serverPath = serverPath;
        }

        public IToken Token => new Token { UserId = _userService.AuthData.Id, Value = _userService.AuthData.Token };

        public async Task<T> GetById(int id)
        {
            var response = await Core.ExecuteGetTaskAsync($"{_serverPath}/api/{typeof(T).Name}/{id}", GetDefaultHeaders());

            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<QueryResult<T>>(response.Content);
                return content.Data;
            }

            throw new QueryException(response.StatusDescription);
        }

        public async Task<IEnumerable<T>> GetByIds(IEnumerable<int> ids)
        {
            var response = await Core.ExecutePostTaskAsync($"{_serverPath}/api/{typeof(T).Name}/GetByIds", ids, GetDefaultHeaders());

            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<QueryResult<IEnumerable<T>>>(response.Content);
                return content.Data;
            }

            throw new QueryException(response.StatusDescription);
        }

        public async Task<IEnumerable<T>> SearchAsync(string searchText)
        {
            var response = await Core.ExecuteGetTaskAsync($"{_serverPath}/api/{typeof(T).Name}?searchText={searchText}", GetDefaultHeaders());

            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<QueryResult<IEnumerable<T>>>(response.Content);
                return content.Data;
            }

            throw new QueryException(response.StatusDescription);
        }

        private (string, string)[] GetDefaultHeaders()
        {
            return new[] { ("Token", Token.ToJsonString()) };
        }
    }
}