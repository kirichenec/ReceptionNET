using Reception.Extension;
using Reception.Server.File.Entities;
using Reception.Server.File.Model.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reception.Server.File.Extensions
{
    public static class FileDataExtension
    {
        public static FileDataDto ToDto(this FileData value)
        {
            return
                value.HasNoValue() ?
                null :
                new FileDataDto
                {
                    Additional = value.Additional,
                    Comment = value.Comment,
                    Data = value.Data,
                    Extension = value.Extension,
                    Id = value.Id,
                    Name = value.Name,
                    Type = value.Type,
                    Version = value.Version
                };
        }

        public async static Task<FileDataDto> ToDtoAsync(this Task<FileData> valueTasked)
        {
            return (await valueTasked).ToDto();
        }

        public static IEnumerable<FileDataDto> ToDtos(this IEnumerable<FileData> valueList)
        {
            return valueList?.Select(value => value.ToDto()).ToList();
        }

        public async static Task<IEnumerable<FileDataDto>> ToDtosAsync(this Task<IEnumerable<FileData>> valueListTasked)
        {
            return (await valueListTasked)?.Select(value => value.ToDto()).ToList();
        }
    }
}