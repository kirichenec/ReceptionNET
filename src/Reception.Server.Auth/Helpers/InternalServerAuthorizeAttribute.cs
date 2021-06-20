using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Reception.Extension.Converters;
using Reception.Model.Interface;
using Reception.Model.Network;
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
            var _tokenService = context.HttpContext.RequestServices.GetRequiredService<ITokenService>();
            
            if (context.HttpContext.Request.Headers["Token"].FirstOrDefault() is string jsonToken)
            {
                var token = jsonToken.DeserializeMessage<Token>();

                if (await _tokenService.CheckAsync(token))
                {
                    context.Result = new JsonResult(new { message = "Authorized" }) { StatusCode = StatusCodes.Status200OK };
                    return;
                }
            }

            // not logged in
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}