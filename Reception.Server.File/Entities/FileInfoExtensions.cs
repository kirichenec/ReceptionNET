using Reception.Server.File.Model.Dto;

namespace Reception.Server.File.Model
{
    public static class FileInfoExtensions
    {
        public static FileVersion ToFileInfo(this FileVersionDto value)
        {
            return new FileVersion
            {
                Id = value.Id,
                Comment = value.Comment,
                Name = value.Name,
                Version = value.Version
            };
        }
    }
}