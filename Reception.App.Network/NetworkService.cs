using Newtonsoft.Json;
using Reception.App.Network.Exceptions;
using Reception.Model.Network;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reception.App.Network
{
    public class NetworkService<T> : INetworkService<T>, IPingService
    {
        public NetworkService(string serverPath)
        {
            ServerPath = serverPath;
        }

        public string ServerPath { get; set; }

        private async Task<IRestResponse> ExecuteGetTaskAsync(string url)
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            var response = await client.ExecuteTaskAsync(request);
            return response;
        }

        public async Task<string> PingAsync()
        {
            var response = await ExecuteGetTaskAsync($"{ServerPath}/api/Helper/Ping");
            return response.Content;
        }

        public async Task<IEnumerable<T>> SearchTAsync(string searchText)
        {
            var response = await ExecuteGetTaskAsync($"{ServerPath}/api/{typeof(T).Name}?searchText={searchText}");

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
                throw new Exception(content.ErrorMessage);
            }
            throw response.ErrorException;
        }
    }
}
