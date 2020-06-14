using Newtonsoft.Json;
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

        public async Task<IEnumerable<T>> GetByIds(IEnumerable<int> ids)
        {
            var response = await Core.ExecuteGetTaskAsync($"{_serverPath}/api/{typeof(T).Name}?searchText={ids}");

            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<QueryResult<List<T>>>(response.Content);
                return content.Data;
            }

            throw response.ErrorException;
        }

        public async Task<IEnumerable<T>> SearchAsync(string searchText)
        {
            var queryPath = $"{_serverPath}/api/{typeof(T).Name}?searchText={searchText}";
            return await GetListQueryAsync(queryPath);
        }

        private static async Task<IEnumerable<T>> GetListQueryAsync(string queryPath)
        {
            var response = await Core.ExecuteGetTaskAsync(queryPath);
            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<QueryResult<List<T>>>(response.Content);
                return content.Data;
            }
            throw response.ErrorException;
        }
    }
}