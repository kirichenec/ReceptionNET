using Reception.Extension;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reception.App.Network
{
    internal static class Core
    {
        internal static async Task<RestResponse> ExecuteGetTaskAsync(string baseUrl, string methodUri,
                                                                     IEnumerable<(string, string)> headers = null)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest(methodUri, Method.Get);
            AddHeaders(request, headers);
            var response = await client.ExecuteAsync(request);
            return response;
        }

        internal static async Task<RestResponse> ExecutePostTaskAsync<T>(string baseUrl, string methodUri, T bodyValue,
                                                                         IEnumerable<(string, string)> headers = null)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest(methodUri, Method.Post);
            request.AddJsonBody(bodyValue);
            AddHeaders(request, headers);
            var response = await client.ExecuteAsync(request);
            return response;
        }

        internal static async Task<RestResponse> ExecutePutTaskAsync<T>(string baseUrl, string methodUri, T bodyValue,
                                                                        IEnumerable<(string, string)> headers = null)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest(methodUri, Method.Put);
            AddHeaders(request, headers);
            request.AddJsonBody(bodyValue);
            var response = await client.ExecuteAsync(request);
            return response;
        }

        private static void AddHeaders(RestRequest request, IEnumerable<(string, string)> headers)
        {
            headers?.ForEach(header => request.AddHeader(header.Item1, header.Item2));
        }
    }
}