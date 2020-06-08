using Reception.Model.Interfaces;
using Reception.Server.File.Model;
using Reception.Server.File.Model.Dto;

namespace Reception.Server.Data.Extensions
{
    public static class VersionInfoExtension
    {
        public static IFileVersionInfo ToDto(this VersionInfo value)
        {
            return
                value == null ?
                null :
                new VersionInfoDto
                {
                    Id = value.Id,
                    Comment = value.Comment,
                    Name = value.Name,
                    Version = value.Version
                };
        }
    }
}