using RestSharp;
using System.Threading.Tasks;

namespace Reception.App.Network.Server
{
    internal static class Core
    {
        internal static async Task<IRestResponse> ExecuteGetTaskAsync(string url)
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            var response = await client.ExecuteAsync(request);
            return response;
        }

        internal static async Task<IRestResponse> ExecutePostTaskAsync<T>(string url, T bodyValue)
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(bodyValue);
            var response = await client.ExecuteAsync(request);
            return response;
        }
    }
} 