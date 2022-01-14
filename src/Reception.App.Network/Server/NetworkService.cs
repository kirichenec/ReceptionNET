using Newtonsoft.Json;
using Reception.App.Network.Auth;
using Reception.App.Network.Exceptions;
using Reception.Model.Network;
using Splat;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reception.App.Network.Server
{
    public class NetworkService<T> : INetworkService<T>
    {
        private readonly IAuthService _authService;
        private readonly string _serverPath;

        public NetworkService(string serverPath)
        {
            _serverPath = serverPath;
            _authService = Locator.Current.GetService<IAuthService>();
        }

        public async Task<T> GetById(int id)
        {
            var response = await Core.ExecuteGetTaskAsync(
                baseUrl: $"{_serverPath}/api/",
                methodUri: $"{typeof(T).Name}/{id}",
                headers: GetDefaultHeaders());

            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<QueryResult<T>>(response.Content);
                return content.Data;
            }

            throw new QueryException(response.StatusDescription, response.StatusCode);
        }

        public async Task<IEnumerable<T>> GetByIds(IEnumerable<int> ids)
        {
            var response = await Core.ExecutePostTaskAsync(
                baseUrl: $"{_serverPath}/api",
                methodUri: $"{typeof(T).Name}/GetByIds",
                bodyValue: ids,
                headers: GetDefaultHeaders());

            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<QueryResult<IEnumerable<T>>>(response.Content);
                return content.Data;
            }

            throw new QueryException(response.StatusDescription, response.StatusCode);
        }

        public async Task<IEnumerable<T>> SearchAsync(string searchText)
        {
            var response = await Core.ExecuteGetTaskAsync(
                baseUrl: $"{_serverPath}/api/",
                methodUri: $"{typeof(T).Name}?searchText={searchText}",
                headers: GetDefaultHeaders());

            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<QueryResult<IEnumerable<T>>>(response.Content);
                return content.Data;
            }

            throw new QueryException(response.StatusDescription, response.StatusCode);
        }

        private (string, string)[] GetDefaultHeaders() => _authService.GetDefaultHeaders();
    }
}