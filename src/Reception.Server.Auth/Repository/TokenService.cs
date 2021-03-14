using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Reception.Extension;
using Reception.Server.Auth.Entities;
using Reception.Server.Auth.Helpers;
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

        public async Task<bool> CheckAsync(Token token)
        {
            var availableToken = await _context.Tokens.FirstOrDefaultAsync(t => t.UserId == token.UserId && t.Value == token.Value);
            return availableToken.HasValue() && IsJwtTokenNotExpired(token.Value);
        }

        public async Task<Token> GenerateAndSaveAsync(int userId)
        {
            var tokenValue = GenerateJwtToken(userId);
            return await SaveAsync(userId, tokenValue);
        }

        #endregion

        #region private

        private static bool IsJwtTokenNotExpired(string value)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadToken(value);
            return DateTime.Now > token.ValidTo;
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

        private async Task<Token> GetTokenAsync(int id)
        {
            return await _context.Tokens.FirstOrDefaultAsync(token => token.Id == id);
        }

        private async Task<Token> SaveAsync(int userId, string value)
        {
            var token = new Token { UserId = userId, Value = value };
            var trackedData = await _context.Tokens.AddAsync(token);
            await _context.SaveChangesAsync();
            return await GetTokenAsync(trackedData.Entity.Id);
        }

        #endregion

        #endregion
    }
}