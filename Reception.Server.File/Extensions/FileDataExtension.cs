using Reception.Server.File.Model;
using Reception.Server.File.Model.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Reception.Server.File.Extensions
{
    public static class FileDataExtension
    {
        public static FileDataDto ToDto(this FileData value)
        {
            return
                value == null ?
                null :
                new FileDataDto
                {
                    Id = value.Id,
                    Value = value.Value,
                    VersionInfo = value.VersionInfo
                };
        }

        public static List<FileDataDto> ToDto(this List<FileData> valueList)
        {
            return valueList.Select(value => value.ToDto()).ToList();
        }
    }
}