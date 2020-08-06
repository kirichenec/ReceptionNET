using Reception.Server.Data.Extensions;
using Reception.Server.File.Model.Dto;
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

        public Task<FileVersionDto> GetAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<FileVersionDto>> GetByIdsAsync(IEnumerable<int> ids)
        {
            throw new System.NotImplementedException();
        }

        public async Task<FileVersionDto> GetInfoAsync(int id)
        {
            var fileVersion = await _dataService.GetAsync(id);
            return fileVersion.ToDto();
        }

        public Task<FileVersionDto> SaveAsync(FileVersionDto value)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<FileVersionDto>> SearchAsync(string searchText)
        {
            var searchedValues = await _dataService.SearchAsync(searchText);
            return searchedValues.ToDtos();
        }
    }
}
