using Reception.Server.Auth.Entities;
using System.Threading.Tasks;

namespace Reception.Server.Auth.Repository
{
    public interface ITokenService
    {
        Task<bool> CheckAsync(string token);
        Task<Token> GenerateAndSaveAsync(int userId);
    }
}