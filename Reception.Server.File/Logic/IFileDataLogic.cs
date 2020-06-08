using Microsoft.AspNetCore.Http;
using Reception.Model.Interfaces;
using System.Threading.Tasks;

namespace Reception.Server.File.Logic
{
    public interface IFileDataLogic
    {
        Task<bool> DeleteFileAsync(int id);
        Task<IFormFile> GetFileAsync(int id);
        Task<IFileVersionInfo> SaveAsync(IFormFile value);
    }
}