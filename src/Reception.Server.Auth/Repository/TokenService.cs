using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Reception.Extension;
using Reception.Model.Interface;
using Reception.Server.Auth.Entities;
using Reception.Server.Auth.Model;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Reception.Server.Auth.Repository
{
    public class TokenService : ITokenService
    {
        private readonly AppSettings _appSettings;
        private readonly UserContext _context;

        public TokenService(IOptions<AppSettings> appSettings, UserContext userContext)
        {
            _appSettings = appSettings.Value;
            _context = userContext;
        }

        #region Methods

        #region public

        public async Task<bool> CheckAsync(IToken token)
        {
            return
                token.HasValue()
                && (await _context.Tokens.FirstOrDefaultAsync(t => t.UserId == token.UserId && t.Value == token.Value)).HasValue()
                && IsJwtTokenNotExpired(token.Value);
        }

        public async Task<IToken> GenerateAndSaveAsync(int userId)
        {
            var tokenValue = GenerateJwtToken(userId);
            return await SaveOrUpdateAsync(userId, tokenValue);
        }

        #endregion

        #region private

        private static bool IsJwtTokenNotExpired(string value)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadToken(value);
            return DateTime.Now <= token.ValidTo;
        }

        private string GenerateJwtToken(int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", userId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(_appSettings.ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private async Task<Token> SaveOrUpdateAsync(int userId, string tokenValue)
        {
            EntityEntry<Token> trackedData;
            if (await GetTokenAsync(userId) is Token existedToken)
            {
                existedToken.Value = tokenValue;
                trackedData = _context.Tokens.Update(existedToken);
            }
            else
            {
                var token = new Token { UserId = userId, Value = tokenValue };
                trackedData = await _context.Tokens.AddAsync(token);
            }
            await _context.SaveChangesAsync();
            return trackedData.Entity;


            async Task<Token> GetTokenAsync(int userId)
            {
                return await _context.Tokens.FirstOrDefaultAsync(token => token.UserId == userId);
            }
        }

        #endregion

        #endregion
    }
}