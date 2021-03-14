using Reception.App.Model.Auth;
using Reception.Model.Interface;
using System.Threading.Tasks;

namespace Reception.App.Network.Auth
{
    public interface IUserService
    {
        AuthenticateResponse AuthData { get; }

        public IToken Token { get; }

        Task<AuthenticateResponse> Authenticate(AuthenticateRequest request);

        Task<bool> IsAuthValid();

        void SetUserAuth(int userId, string token);
    }
}