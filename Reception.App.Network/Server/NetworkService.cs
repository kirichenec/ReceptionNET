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

        public async Task<IEnumerable<T>> SearchAsync(string searchText)
        {
            var response = await Core.ExecuteGetTaskAsync($"{_serverPath}/api/{typeof(T).Name}?searchText={searchText}");

            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<QueryResult<List<T>>>(response.Content);
                return content.Data;
            }
            throw response.ErrorException;
        }
    }
}