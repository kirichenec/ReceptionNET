using Reception.App.Model.Auth;

namespace Reception.App.Network.Auth
{
    public interface IAuthService
    {
        AuthenticateResponse AuthData { get; }

        Task<AuthenticateResponse> Authenticate(string login, string password);
        public (string, string)[] GetDefaultHeaders();
        Task<bool> IsAuthValid();
    }
}