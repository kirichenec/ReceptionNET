using Reception.Server.File.Entities;
using Reception.Server.File.Model.Dto;

namespace Reception.Server.File.Mapper;

public static class FileDataDtoMapper
{
    public static FileDataDto Map(this FileData source)
    {
        return new()
        {
            Comment = source.Comment,
            Data = source.Data,
            Extension = source.Extension,
            Id = source.Id,
            Name = source.Name,
            Type = source.Type,
            Version = source.Version,
        };
    }

    public static FileData Map(this FileDataDto source)
    {
        return new()
        {
            Comment = source.Comment,
            Data = source.Data,
            Extension = source.Extension,
            Id = source.Id,
            Name = source.Name,
            Type = source.Type,
            Version = source.Version,
        };
    }

    public static IEnumerable<FileDataDto> Map(this IEnumerable<FileData> source)
    {
        return source.Select(Map);
    }
}
