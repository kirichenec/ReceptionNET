using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Reception.Extension;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reception.Server.Auth.ConnectionLibrary
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var authSettings = context.HttpContext.RequestServices.GetRequiredService<IOptions<AuthSettings>>().Value;

            var jsonToken = context.HttpContext.Request.Headers["Token"].FirstOrDefault();
#if DEBUG
            // ToDo: Client must send token to server each time
            jsonToken = "{\"UserId\":1,\"Value\":\"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJuYmYiOjE2MTU3MjcxNTYsImV4cCI6MTYzMDI0MjM1NiwiaWF0IjoxNjE1NzI3MTU2fQ.Je6PB3jKZDG2MMXyFl6suYTy8f2ru3Ldx9AuArkwA1M\"}";
#endif

            var authCheckResult =
                jsonToken.HasValue()
                && await CheckAuth(authSettings.AuthServerPath, new[] { ("Token", jsonToken) });

            if (!authCheckResult)
            {
                // not logged in
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }

        private async Task<bool> CheckAuth(string authServerPath, IEnumerable<(string, string)> headers)
        {
            var client = new RestClient($"{authServerPath}/User/IsAuthValid");
            var request = new RestRequest(Method.GET);
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