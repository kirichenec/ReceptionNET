using Reception.App.Model.Auth;
using System.Threading.Tasks;

namespace Reception.App.Network.Auth
{
    public interface IUserService
    {
        AuthenticateResponse AuthData { get; }

        Task<AuthenticateResponse> Authenticate(AuthenticateRequest request);

        Task<bool> IsAuthValid();

        void SetUserAuth(int userId, string token);
    }
}