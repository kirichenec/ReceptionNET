using Reception.Server.File.Model;
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

        public async Task<FileData> GetAsync(int id)
        {
            var fileData = await _dataService.GetAsync(id);
            return fileData;
        }

        public async Task<FileData> SaveAsync(string fileName, byte[] fileData)
        {
            var data = new FileData
            {
                Value = fileData,
                VersionInfo = new FileVersion { Name = fileName, Version = Guid.NewGuid() }
            };
            var result = await _dataService.SaveAsync(data);
            return result;
        }

        public Task<FileData> SaveAsync(FileData value)
        {
            var rightMethodInfo = GetType().GetMethod(nameof(SaveAsync), new Type[] { typeof(string), typeof(byte[]) });
            var wrongMethodInfo = GetType().GetMethod(nameof(SaveAsync), new Type[] { typeof(FileData) });
            throw new NotSupportedException($"Use {rightMethodInfo} instead of {wrongMethodInfo}");
        }

        public Task<List<FileData>> SearchAsync(string searchText)
        {
            throw new NotImplementedException();
        }
    }
}