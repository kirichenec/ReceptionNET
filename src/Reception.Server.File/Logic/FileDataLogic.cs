using Reception.Server.File.Extensions;
using Reception.Server.File.Model;
using Reception.Server.File.Model.Dto;
using Reception.Server.File.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reception.Server.File.Logic
{
    public class FileDataLogic : IFileDataLogic
    {
        private readonly IFileDataService _dataService;

        public FileDataLogic(IFileDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _dataService.DeleteAsync(id);
        }

        public async Task<FileDataDto> GetAsync(int id)
        {
            var fileData = await _dataService.GetAsync(id);
            return fileData.ToDto();
        }

        public Task<IEnumerable<FileDataDto>> GetByIdsAsync(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public async Task<FileDataDto> SaveAsync(string fileName, byte[] fileData)
        {
            var data = new FileData
            {
                Value = fileData,
                VersionInfo = new FileVersion { Name = fileName, Version = Guid.NewGuid() }
            };
            var result = await _dataService.SaveAsync(data).ToDtoAsync();
            return result;
        }

        public Task<FileDataDto> SaveAsync(FileDataDto value)
        {
            var rightMethodInfo = GetType().GetMethod(nameof(SaveAsync), new Type[] { typeof(string), typeof(byte[]) });
            var wrongMethodInfo = GetType().GetMethod(nameof(SaveAsync), new Type[] { typeof(FileDataDto) });
            throw new NotSupportedException($"Use {rightMethodInfo} instead of {wrongMethodInfo}");
        }

        public async Task<IEnumerable<FileDataDto>> SearchAsync(string searchText)
        {
            var searchedValues = await _dataService.SearchAsync(searchText).ToDtosAsync();
            return searchedValues;
        }
    }
}