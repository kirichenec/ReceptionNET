using AutoMapper;
using Reception.Server.File.Entities;
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
        private readonly IMapper _mapper;

        public FileDataLogic(IFileDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _dataService.DeleteAsync(id);
        }

        public async Task<FileDataDto> GetAsync(int id)
        {
            var fileData = await _dataService.GetAsync(id);
            return _mapper.Map<FileDataDto>(fileData);
        }

        public Task<IEnumerable<FileDataDto>> GetByIdsAsync(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public async Task<FileDataDto> SaveAsync(string fileName, byte[] fileData)
        {
            var data = new FileData
            {
                Data = fileData
            };
            return _mapper.Map<FileDataDto>(await _dataService.SaveAsync(data));
        }

        public Task<FileDataDto> SaveAsync(FileDataDto value)
        {
            var rightMethodInfo = GetType().GetMethod(nameof(SaveAsync), new Type[] { typeof(string), typeof(byte[]) });
            var wrongMethodInfo = GetType().GetMethod(nameof(SaveAsync), new Type[] { typeof(FileDataDto) });
            throw new NotSupportedException($"Use {rightMethodInfo} instead of {wrongMethodInfo}");
        }

        public async Task<IEnumerable<FileDataDto>> SearchAsync(string searchText)
        {
            var searchedValues = _mapper.Map<IEnumerable<FileDataDto>>(await _dataService.SearchAsync(searchText));
            return searchedValues;
        }
    }
}