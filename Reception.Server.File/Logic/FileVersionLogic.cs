using Reception.Model.Interfaces;
using Reception.Server.Data.Extensions;
using Reception.Server.File.Repository;
using System.Threading.Tasks;

namespace Reception.Server.File.Logic
{
    public class FileVersionLogic : IFileVersionLogic
    {
        private readonly IFileVersionService _dataService;

        public FileVersionLogic(IFileVersionService dataService)
        {
            _dataService = dataService;
        }

        public async Task<IFileVersionInfo> GetInfoAsync(int id)
        {
            return (await _dataService.GetAsync(id)).ToDto();
        }
    }
}
