using Reception.Model.Interface;
using Reception.Server.File.Entities;
using System.Threading.Tasks;

namespace Reception.Server.File.Repository
{
    public interface IFileDataService : IRepository<FileData>
    {
        Task<bool> DeleteAsync(int id);
        Task<FileData> SaveAsync(FileData value);
    }
}