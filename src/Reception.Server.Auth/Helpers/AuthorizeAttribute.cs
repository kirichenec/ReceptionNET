using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Reception.Extension.Converters;
using Reception.Server.Auth.Entities;
using Reception.Server.Auth.Repository;
using System;
using System.Linq;

namespace Reception.Server.Auth.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var _tokenService = context.HttpContext.RequestServices.GetRequiredService<ITokenService>();

            if (context.HttpContext.Request.Headers["Token"].FirstOrDefault() is string jsonToken)
            {
                var token = jsonToken.DeserializeMessage<Token>();

                // ToDo: Maybe async?
                if (_tokenService.CheckAsync(token).Result)
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