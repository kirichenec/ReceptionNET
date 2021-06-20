using Reception.Model.Interface;
using System.Threading.Tasks;

namespace Reception.Core.Auth.Repository
{
    public interface ITokenService
    {
        Task<bool> CheckAsync(IToken token);
        Task<IToken> GenerateAndSaveAsync(int userId);
    }
}