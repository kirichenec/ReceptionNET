using Reception.Model.Interface;
using Reception.Core.Auth.Model;
using System.Threading.Tasks;

namespace Reception.Core.Auth.Logic
{
    public interface IUserLogic : ILogic<UserDto>
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest requestModel);
        Task<UserDto> CreateUserAsync(string login, string password);
    }
}