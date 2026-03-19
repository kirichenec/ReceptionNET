using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Reception.Extension;
using Reception.Model.Dto;
using Reception.Model.Network;
using Reception.Server.Auth.Entities;
using Reception.Server.Auth.Mapper;
using Reception.Server.Auth.PasswordHelper;
using Reception.Server.Auth.Repository;

namespace Reception.Server.Auth.Logic;

public class UserLogic(
    IUserService userService,
    ITokenService tokenService,
    IOptions<HashingOptions> hashingOptions
    ) : IUserLogic
{
    private readonly PasswordHasher _passwordHasher = new(hashingOptions.Value);
    private readonly ITokenService _tokenService = tokenService;
    private readonly IUserService _userService = userService;


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

        return user.Map(token.Value);
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
        var user = await _userService.GetAsync(id, cancellationToken);
        return user.Map();
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
        var user = await _userService.SaveAsync(data, cancellationToken);
        return user.Map();
    }

    public async Task<IEnumerable<UserDto>> SearchAsync(string searchText,
        CancellationToken cancellationToken = default)
    {
        var users = await _userService.SearchAsync(searchText, cancellationToken);
        return users.Map();
    }
}
