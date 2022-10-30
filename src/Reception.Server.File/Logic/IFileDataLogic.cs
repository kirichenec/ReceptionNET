using Reception.Server.Core.Interfaces;
using Reception.Server.File.Model.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace Reception.Server.File.Logic
{
    public interface IFileDataLogic : ILogic<FileDataDto>
    {
        Task<FileDataDto> SaveAsync(string fileName, byte[] fileData,
            CancellationToken cancellationToken = default);
    }
}