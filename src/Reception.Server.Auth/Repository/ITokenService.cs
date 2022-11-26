using Reception.Server.Auth.Entities;

namespace Reception.Server.Auth.Repository
{
    public interface ITokenService
    {
        Task<bool> CheckAsync(string token, CancellationToken cancellationToken = default);
        Task<Token> GenerateAndSaveAsync(int userId, CancellationToken cancellationToken = default);
    }
}