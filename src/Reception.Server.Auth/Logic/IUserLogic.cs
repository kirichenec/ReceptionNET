using Reception.Model.Interface;
using Reception.Server.Auth.Models;
using System.Threading.Tasks;

namespace Reception.Server.Auth.Logic
{
    public interface IUserLogic : ILogic<UserDto>
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest requestModel);
        Task<UserDto> CreateUserAsync(string login, string password);
    }
}