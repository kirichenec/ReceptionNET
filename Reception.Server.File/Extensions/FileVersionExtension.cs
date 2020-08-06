using Reception.Server.File.Model;
using Reception.Server.File.Model.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Reception.Server.Data.Extensions
{
    public static class FileVersionExtension
    {
        public static FileVersionDto ToDto(this FileVersion value)
        {
            return
                value == null ?
                null :
                new FileVersionDto
                {
                    Id = value.Id,
                    Comment = value.Comment,
                    Name = value.Name,
                    Version = value.Version
                };
        }

        public static IEnumerable<FileVersionDto> ToDtos(this IEnumerable<FileVersion> valueList)
        {
            return valueList?.Select(value => value.ToDto()).ToList();
        }
    }
}