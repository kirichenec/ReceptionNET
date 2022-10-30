using Reception.Server.Core.Interfaces;
using Reception.Server.File.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Reception.Server.File.Repository
{
    public interface IFileDataService : IRepository<FileData>
    {
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<FileData> SaveAsync(FileData value, CancellationToken cancellationToken = default);
    }
}