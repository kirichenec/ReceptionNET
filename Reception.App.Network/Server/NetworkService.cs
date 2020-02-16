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
         
        public async Task<IEnumerable<T>> SearchTAsync(string searchText)
        {
            var response = await Core.ExecuteGetTaskAsync($"{_serverPath}/api/{typeof(T).Name}?searchText={searchText}");

            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<QueryResult<List<T>>>(response.Content);
                if (content.ErrorCode == ErrorCode.Ok)
                {
                    return content.Data;
                }
                if (content.ErrorCode == ErrorCode.NotFound)
                {
                    throw new NotFoundException<T>(ErrorCode.NotFound.ToString());
                }
                throw new QueryException(content.ErrorMessage);
            }
            throw response.ErrorException;
        }
    }
}