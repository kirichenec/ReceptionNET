using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Reception.Server.Auth.Entities;
using Reception.Server.Auth.Extensions;
using Reception.Server.Auth.Helpers;
using Reception.Server.Auth.Models;
using Reception.Server.Auth.PasswordHelper;
using Reception.Server.Auth.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Reception.Server.Auth.Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly AppSettings _appSettings;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserService _userService;

        public UserLogic(IOptions<AppSettings> appSettings, IUserService userService, IOptions<HashingOptions> hashingOptions)
        {
            _appSettings = appSettings.Value;
            _passwordHasher = new PasswordHasher(hashingOptions.Value);
            _userService = userService;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest requestModel)
        {
            var userName = requestModel.Username;
            var user = await
                _userService.Queryable().SingleOrDefaultAsync(x => x.Username == userName)
                .ToDtoAsync();

            // return null if user not found
            if (user == null) return null;

            var (verified, needsUpgrade) = _passwordHasher.Check(user.Password, requestModel.Password);
            if (!verified || needsUpgrade) return null;

            // authentication successful so generate jwt token
            var token = GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public async Task<UserDto> CreateUserAsync(string username, string password)
        {
            var userDto = new UserDto { Username = username, Password = password };
            return await SaveAsync(userDto);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await Task.FromResult(true);
        }

        public async Task<UserDto> GetAsync(int id)
        {
            return await _userService.GetAsync(id).ToDtoAsync();
        }

        public Task<IEnumerable<UserDto>> GetByIdsAsync(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDto> SaveAsync(UserDto value)
        {
            var data = new User
            {
                FirstName = value.FirstName,
                LastName = value.LastName,
                MiddleName = value.MiddleName,
                Password = _passwordHasher.Hash(value.Password),
                Username = value.Username
            };
            var result = await _userService.SaveAsync(data).ToDtoAsync();
            return result;
        }

        public async Task<IEnumerable<UserDto>> SearchAsync(string searchText)
        {
            return await _userService.SearchAsync(searchText).ToDtosAsync();
        }

        private string GenerateJwtToken(UserDto user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}