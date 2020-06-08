using Reception.Model.Interfaces;
using System.Threading.Tasks;

namespace Reception.Server.File.Logic
{
    public interface IFileVersionLogic
    {
        Task<IFileVersionInfo> GetInfoAsync(int id);
    }
}