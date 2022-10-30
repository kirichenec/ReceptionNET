using Reception.Model.Dto;
using Reception.Model.Network;
using Reception.Server.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Reception.Server.Auth.Logic
{
    public interface IUserLogic : ILogic<UserDto>
    {
        Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest requestModel, CancellationToken cancellationToken = default);
        Task<UserDto> CreateUserAsync(string login, string password, CancellationToken cancellationToken = default);
    }
}