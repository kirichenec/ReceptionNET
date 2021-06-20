using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Reception.Core.Auth.Logic;
using Reception.Core.Auth.Model;
using Reception.Core.Auth.Repository;
using Reception.Extension;
using Reception.Server.Auth.Entities;
using Reception.Server.Auth.Extensions;
using Reception.Server.Auth.PasswordHelper;
using Reception.Server.Auth.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reception.Server.Auth.Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public UserLogic(IUserService userService, ITokenService tokenService, IOptions<HashingOptions> hashingOptions)
        {
            _passwordHasher = new PasswordHasher(hashingOptions.Value);
            _tokenService = tokenService;
            _userService = userService;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest requestModel)
        {
            var user = await _userService.Queryable()
                .SingleOrDefaultAsync(x => x.Login == requestModel.Login)
                .ToDtoAsync();

            // return null if user not found
            if (user.HasNoValue()) return null;

            var (verified, needsUpgrade) = _passwordHasher.Check(user.Password, requestModel.Password);
            if (!verified || needsUpgrade) return null;

            // authentication successful so generate jwt token
            var token = await _tokenService.GenerateAndSaveAsync(user.Id);

            return new AuthenticateResponse(user, token.Value);
        }

        public async Task<UserDto> CreateUserAsync(string login, string password)
        {
            var userDto = new UserDto { Login = login, Password = password };
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
                Login = value.Login,
                MiddleName = value.MiddleName,
                Password = _passwordHasher.Hash(value.Password)
            };
            var result = await _userService.SaveAsync(data).ToDtoAsync();
            return result;
        }

        public async Task<IEnumerable<UserDto>> SearchAsync(string searchText)
        {
            return await _userService.SearchAsync(searchText).ToDtosAsync();
        }
    }
}