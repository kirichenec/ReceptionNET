using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Reception.Extension;
using Reception.Model.Dto;
using Reception.Model.Network;
using Reception.Server.Auth.Entities;
using Reception.Server.Auth.MapperProfiles;
using Reception.Server.Auth.PasswordHelper;
using Reception.Server.Auth.Repository;
using Reception.Server.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reception.Server.Auth.Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public UserLogic(IUserService userService, ITokenService tokenService, IOptions<HashingOptions> hashingOptions, IMapper mapper)
        {
            _mapper = mapper;
            _passwordHasher = new PasswordHasher(hashingOptions.Value);
            _tokenService = tokenService;
            _userService = userService;
        }

        public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest requestModel,
            CancellationToken cancellationToken = default)
        {
            var user = await _userService.Queryable()
                .SingleOrDefaultAsync(x => x.Login == requestModel.Login, cancellationToken);

            // return null if user not found
            if (user.HasNoValue()) return null;

            var (verified, needsUpgrade) = _passwordHasher.Check(user.Password, requestModel.Password);
            if (!verified || needsUpgrade) return null;

            // authentication successful so generate jwt token
            var token = await _tokenService.GenerateAndSaveAsync(user.Id, cancellationToken);

            return _mapper.Map<User, AuthenticateResponse>(user, (AutoMapperProfile.TOKEN_OPTION_NAME, token.Value));
        }

        public async Task<UserDto> CreateUserAsync(string login, string password,
            CancellationToken cancellationToken = default)
        {
            var userDto = new UserDto { Login = login, Password = password };
            return await SaveAsync(userDto, cancellationToken);
        }

        public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDto> GetAsync(int id,
            CancellationToken cancellationToken = default)
        {
            return _mapper.Map<UserDto>(await _userService.GetAsync(id, cancellationToken));
        }

        public Task<IEnumerable<UserDto>> GetByIdsAsync(IEnumerable<int> ids,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDto> SaveAsync(UserDto value, CancellationToken cancellationToken = default)
        {
            var data = new User
            {
                FirstName = value.FirstName,
                LastName = value.LastName,
                Login = value.Login,
                MiddleName = value.MiddleName,
                Password = _passwordHasher.Hash(value.Password)
            };
            return _mapper.Map<UserDto>(await _userService.SaveAsync(data, cancellationToken));
        }

        public async Task<IEnumerable<UserDto>> SearchAsync(string searchText,
            CancellationToken cancellationToken = default)
        {
            return _mapper.Map<IEnumerable<UserDto>>(await _userService.SearchAsync(searchText, cancellationToken));
        }
    }
}