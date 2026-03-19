using Reception.Server.File.Entities;
using Reception.Server.File.Mapper;
using Reception.Server.File.Model.Dto;
using Reception.Server.File.Repository;

namespace Reception.Server.File.Logic;

public class FileDataLogic(IFileDataService dataService) : IFileDataLogic
{
    private readonly IFileDataService _dataService = dataService;


    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dataService.DeleteAsync(id, cancellationToken);
    }

    public async Task<FileDataDto> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var fileData = await _dataService.GetAsync(id, cancellationToken);
        return fileData.Map();
    }

    public Task<IEnumerable<FileDataDto>> GetByIdsAsync(IEnumerable<int> ids,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<FileDataDto> SaveAsync(string fileName, byte[] fileData,
        CancellationToken cancellationToken = default)
    {
        var data = new FileData { Data = fileData };
        var file = await _dataService.SaveAsync(data, cancellationToken);
        return file.Map();
    }

    public Task<FileDataDto> SaveAsync(FileDataDto value, CancellationToken cancellationToken = default)
    {
        var rightMethodInfo = GetType().GetMethod(nameof(SaveAsync), [typeof(string), typeof(byte[])]);
        var wrongMethodInfo = GetType().GetMethod(nameof(SaveAsync), [typeof(FileDataDto)]);
        throw new NotSupportedException($"Use {rightMethodInfo} instead of {wrongMethodInfo}");
    }

    public async Task<IEnumerable<FileDataDto>> SearchAsync(string searchText,
        CancellationToken cancellationToken = default)
    {
        var files = await _dataService.SearchAsync(searchText, cancellationToken);
        return files.Map();
    }
}
