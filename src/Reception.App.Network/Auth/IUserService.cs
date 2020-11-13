using Reception.App.Model.Auth;
using System.Threading.Tasks;

namespace Reception.App.Network.Auth
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest request);
    }
}