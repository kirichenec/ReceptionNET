using Reception.Extension;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reception.App.Network
{
    internal static class Core
    {
        internal static async Task<IRestResponse> ExecuteGetTaskAsync(string url, IEnumerable<(string, string)> headers = null)
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            AddHeaders(request, headers);
            var response = await client.ExecuteAsync(request);
            return response;
        }

        internal static async Task<IRestResponse> ExecutePostTaskAsync<T>(string url, T bodyValue, IEnumerable<(string, string)> headers = null)
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(bodyValue);
            AddHeaders(request, headers);
            var response = await client.ExecuteAsync(request);
            return response;
        }

        internal static async Task<IRestResponse> ExecutePutTaskAsync<T>(string url, T bodyValue, IEnumerable<(string, string)> headers = null)
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.PUT);
            AddHeaders(request, headers);
            request.AddJsonBody(bodyValue);
            var response = await client.ExecuteAsync(request);
            return response;
        }

        private static void AddHeaders(RestRequest request, IEnumerable<(string, string)> headers)
        {
            headers?.ForEach(header => request.AddHeader(header.Item1, header.Item2));
        }
    }}