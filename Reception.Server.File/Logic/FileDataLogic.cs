using Microsoft.AspNetCore.Http;
using Reception.Extensions;
using Reception.Model.Interfaces;
using Reception.Server.Data.Extensions;
using Reception.Server.File.Model;
using Reception.Server.File.Repository;
using System;
using System.IO;
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

        public async Task<bool> DeleteFileAsync(int id)
        {
            return await _dataService.DeleteAsync(id);
        }

        public async Task<IFormFile> GetFileAsync(int id)
        {
            var fileData = await _dataService.GetAsync(id);
            var stream = new MemoryStream(fileData.Value);
            IFormFile file = new FormFile(stream, 0, fileData.Value.Length, fileData.VersionInfo.Name, fileData.VersionInfo.FileName);
            return file;
        }

        public async Task<IFileVersionInfo> SaveAsync(IFormFile value)
        {
            var data = new FileData
            {
                Value = await value.GetBytes(),
                VersionInfo = new VersionInfo { FileName = value.FileName, Name = value.Name, Version = Guid.NewGuid() }
            };
            var result = await _dataService.SaveAsync(data);
            return result.VersionInfo.ToDto();
        }
    }
}