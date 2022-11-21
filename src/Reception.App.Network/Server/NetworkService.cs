using Newtonsoft.Json;
using Reception.App.Network.Auth;
using Reception.App.Network.Exceptions;
using Reception.Model.Network;
using RestSharp;
using Splat;

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

        public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var response = await Core.ExecuteGetTaskAsync(
                baseUrl: $"{_serverPath}/api",
                methodUri: $"{typeof(T).Name}/{id}",
                headers: GetDefaultHeaders(),
                cancellationToken);

            return ValidateAndGetResponseData<T>(response);
        }

        public async Task<IEnumerable<T>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default)
        {
            var response = await Core.ExecutePostTaskAsync(
                baseUrl: $"{_serverPath}/api",
                methodUri: $"{typeof(T).Name}/GetByIds",
                bodyValue: ids,
                headers: GetDefaultHeaders(),
                cancellationToken);

            return ValidateAndGetResponseData<IEnumerable<T>>(response);
        }

        public async Task<IEnumerable<T>> SearchAsync(string searchText, CancellationToken cancellationToken = default)
        {
            var response = await Core.ExecuteGetTaskAsync(
                baseUrl: $"{_serverPath}/api",
                methodUri: $"{typeof(T).Name}?searchText={searchText}",
                headers: GetDefaultHeaders(),
                cancellationToken);

            return ValidateAndGetResponseData<IEnumerable<T>>(response);
        }

        private (string, string)[] GetDefaultHeaders() => _authService.GetDefaultHeaders();

        private static TOut ValidateAndGetResponseData<TOut>(RestResponse response)
        {
            return response.ResponseStatus switch
            {
                ResponseStatus.Completed => JsonConvert.DeserializeObject<QueryResult<TOut>>(response.Content).Data,
                ResponseStatus.Aborted => throw new OperationCanceledException(response.ErrorMessage),
                _ => throw new QueryException(response.StatusDescription, response.StatusCode),
            };
        }
    }
}
