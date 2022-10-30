using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Reception.Constant;
using Reception.Model.Network;
using Reception.Server.Auth.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Reception.Server.Auth.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    internal class InternalServerAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var tokenService = context.HttpContext.RequestServices.GetRequiredService<ITokenService>();
            var cancellationToken = context.HttpContext.RequestAborted;

            if (context.HttpContext.Request.Headers[HttpHeaders.TOKEN].FirstOrDefault() is not string token
                || !await tokenService.CheckAsync(token, cancellationToken))
            {
                // not logged in
                context.Result = DefaultResponse.UNAUTHORIZED_RESULT;
            }
        }
    }
}
