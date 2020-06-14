using Reception.Model.Interfaces;
using Reception.Server.File.Model;
using System.Threading.Tasks;

namespace Reception.Server.File.Logic
{
    public interface IFileDataLogic : ILogic<FileData>
    {
        Task<FileData> SaveAsync(string fileName, byte[] fileData);
    }
}