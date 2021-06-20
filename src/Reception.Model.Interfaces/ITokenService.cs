using System.Threading.Tasks;

namespace Reception.Model.Interface
{
    public interface ITokenService
    {
        Task<bool> CheckAsync(IToken token);
        Task<IToken> GenerateAndSaveAsync(int userId);
    }
}