﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Reception.Constant;
using Reception.Extension;
using Reception.Server.Auth.Logic;
using Reception.Server.Auth.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Reception.Server.Auth.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IUserLogic userService)
        {
            if (context.Request.Headers[HttpHeaders.TOKEN].FirstOrDefault() is string token)
            {
                await AttachUserToContext(context, userService, token);
            }

            await _next(context);
        }

        private async Task AttachUserToContext(HttpContext context, IUserLogic userService, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == Constants.ClaimTypes.USER_ID).Value.ParseInt();

                // attach user to context on successful jwt validation
                context.Items["User"] = await userService.GetAsync(userId);
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}