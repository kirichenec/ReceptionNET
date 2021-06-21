using Reception.Model.Dto;
using Reception.Model.Interface;
using Reception.Model.Network;
using System.Threading.Tasks;

namespace Reception.Server.Auth.Logic
{
    public interface IUserLogic : ILogic<UserDto>
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest requestModel);
        Task<UserDto> CreateUserAsync(string login, string password);
    }
}