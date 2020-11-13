using Newtonsoft.Json;
using Reception.App.Network.Exceptions;
using Reception.Model.Network;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reception.App.Network.Server
{
    public class NetworkService<T> : INetworkService<T>
    {
        private readonly string _serverPath;

        public NetworkService(string serverPath)
        {
            _serverPath = serverPath;
        }

        public async Task<T> GetById(int id)
        {
            var response = await Core.ExecuteGetTaskAsync($"{_serverPath}/api/{typeof(T).Name}/{id}");

            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<QueryResult<T>>(response.Content);
                return content.Data;
            }

            throw new QueryException(response.StatusDescription);
        }

        public async Task<IEnumerable<T>> GetByIds(IEnumerable<int> ids)
        {
            var response = await Core.ExecutePostTaskAsync($"{_serverPath}/api/{typeof(T).Name}/GetByIds", ids);

            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<QueryResult<IEnumerable<T>>>(response.Content);
                return content.Data;
            }

            throw new QueryException(response.StatusDescription);
        }

        public async Task<IEnumerable<T>> SearchAsync(string searchText)
        {
            var response = await Core.ExecuteGetTaskAsync($"{_serverPath}/api/{typeof(T).Name}?searchText={searchText}");

            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<QueryResult<IEnumerable<T>>>(response.Content);
                return content.Data;
            }

            throw new QueryException(response.StatusDescription);
        }
    }
}