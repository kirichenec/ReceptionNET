using Reception.Server.Auth.Entities;
using System.Threading.Tasks;

namespace Reception.Server.Auth.Repository
{
    public interface ITokenService
    {
        // ToDo: maybe we don't needs it
        Task<bool> CheckAsync(Token token);
        Task<Token> GenerateAndSaveAsync(int userId);
    }
}