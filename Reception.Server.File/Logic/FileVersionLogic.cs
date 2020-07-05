using Reception.Server.File.Model;
using Reception.Server.File.Repository;
using System.Collections.Generic;
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

        public Task<bool> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<FileVersion> GetAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<FileVersion> GetInfoAsync(int id)
        {
            return (await _dataService.GetAsync(id));
        }

        public Task<FileVersion> SaveAsync(FileVersion value)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<FileVersion>> SearchAsync(string searchText)
        {
            return await _dataService.SearchAsync(searchText);
        }
    }
}
