using Newtonsoft.Json;
using Reception.App.Network.Exceptions;
using Reception.Model.Network;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reception.App.Network
{
    public class NetworkServise<T> : INetworkServise<T>
    {
        public NetworkServise(string serverPath)
        {
            ServerPath = serverPath;
        }

        public string ServerPath { get; set; }

        public async Task<IEnumerable<T>> SearchTAsync(string searchText)
        {
            var client = new RestClient($"{ServerPath}/api/{typeof(T).Name}?searchText={searchText}");
            var request = new RestRequest(Method.GET);
            var response = await client.ExecuteTaskAsync(request);

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
