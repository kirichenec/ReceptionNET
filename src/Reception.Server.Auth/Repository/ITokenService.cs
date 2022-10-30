using Reception.Server.Auth.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Reception.Server.Auth.Repository
{
    public interface ITokenService
    {
        Task<bool> CheckAsync(string token, CancellationToken cancellationToken = default);
        Task<Token> GenerateAndSaveAsync(int userId, CancellationToken cancellationToken = default);
    }
}