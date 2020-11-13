using Reception.Model.Interfaces;
using Reception.Server.Auth.Models;
using System.Threading.Tasks;

namespace Reception.Server.Auth.Logic
{
    public interface IUserLogic : ILogic<UserDto>
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest requestModel);
        Task<UserDto> CreateUserAsync(string username, string password);
    }
}