using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Reception.Constant;
using Reception.Extension;
using Reception.Model.Network;
using RestSharp;

namespace Reception.Server.Auth.ConnectionLibrary
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var authSettings = context.HttpContext.RequestServices.GetRequiredService<IOptions<AuthSettings>>().Value;

            if (context.HttpContext.Request.Headers[HttpHeaders.TOKEN].FirstOrDefault() is not string token
                || !await CheckAuth(authSettings.AuthServerPath, new[] { (HttpHeaders.TOKEN, token) }))
            {
                // not logged in
                context.Result = DefaultResponse.UNAUTHORIZED_RESULT;
            }
        }

        private async Task<bool> CheckAuth(string authServerPath, IEnumerable<(string, string)> headers)
        {
            var client = new RestClient(authServerPath);
            var request = new RestRequest("User/IsAuthValid", Method.Get);
            AddHeaders(request, headers);
            var response = await client.ExecuteAsync(request);
            return response.IsSuccessful;
        }

        private static void AddHeaders(RestRequest request, IEnumerable<(string, string)> headers)
        {
            headers?.ForEach(header => request.AddHeader(header.Item1, header.Item2));
        }
    }
}