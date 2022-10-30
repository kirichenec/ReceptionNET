using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Reception.Extension;
using Reception.Server.Auth.Entities;
using Reception.Server.Auth.Model;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reception.Server.Auth.Repository
{
    public class TokenService : ITokenService
    {
        private readonly AppSettings _appSettings;
        private readonly AuthContext _context;

        public TokenService(IOptions<AppSettings> appSettings, AuthContext userContext)
        {
            _appSettings = appSettings.Value;
            _context = userContext;
        }

        #region Methods

        #region public

        public async Task<bool> CheckAsync(string token, CancellationToken cancellationToken = default)
        {
            return
                !token.IsNullOrWhiteSpace()
                && ReadToken(token) is JwtSecurityToken jwtToken
                && GetUserId(jwtToken) is int userId
                && (await GetTokenAsync(token, userId, cancellationToken)).HasValue()
                && IsJwtTokenActual(jwtToken);


            Task<Token> GetTokenAsync(string token, int userId, CancellationToken cancellationToken)
            {
                return _context.Tokens.SingleOrDefaultAsync(t => t.Value == token && t.UserId == userId, cancellationToken);
            }
        }

        public async Task<Token> GenerateAndSaveAsync(int userId, CancellationToken cancellationToken = default)
        {
            var tokenValue = GenerateJwtToken(userId);
            return await SaveOrUpdateAsync(userId, tokenValue, cancellationToken);
        }

        #endregion

        #region private

        private static bool IsJwtTokenActual(JwtSecurityToken token)
        {
            return DateTime.Now.Between(token.ValidFrom, token.ValidTo);
        }

        private static int? GetUserId(JwtSecurityToken jwtToken)
        {
            var userClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == Constants.ClaimTypes.USER_ID);
            return userClaim?.Value.ParseInt();
        }

        private static JwtSecurityToken ReadToken(string value)
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(value);
        }

        private string GenerateJwtToken(int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(Constants.ClaimTypes.USER_ID, userId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(_appSettings.ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private async Task<Token> SaveOrUpdateAsync(int userId, string tokenValue,
            CancellationToken cancellationToken = default)
        {
            EntityEntry<Token> trackedData;
            if (await GetTokenAsync(userId, cancellationToken) is Token existedToken)
            {
                existedToken.Value = tokenValue;
                trackedData = _context.Tokens.Update(existedToken);
            }
            else
            {
                var token = new Token { UserId = userId, Value = tokenValue };
                trackedData = await _context.Tokens.AddAsync(token, cancellationToken);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return trackedData.Entity;


            async Task<Token> GetTokenAsync(int userId, CancellationToken cancellationToken = default)
            {
                return await _context.Tokens.FirstOrDefaultAsync(token => token.UserId == userId, cancellationToken);
            }
        }

        #endregion

        #endregion
    }
}
