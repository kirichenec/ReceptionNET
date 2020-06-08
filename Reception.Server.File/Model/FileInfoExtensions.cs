using Reception.Server.File.Model.Dto;

namespace Reception.Server.File.Model
{
    public static class FileInfoExtensions
    {
        public static VersionInfo ToFileInfo(this VersionInfoDto value)
        {
            return new VersionInfo
            {
                Id = value.Id,
                Comment = value.Comment,
                FileName = value.FileName,
                Name = value.Name,
                Version = value.Version
            };
        }
    }
}