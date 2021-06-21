using Reception.Model.Interface;

namespace Reception.Server.File.Model.Dto
{
    public class FileDataDto : IUnique
    {
        public int Id { get; set; }

        public byte[] Value { get; set; }

        public IFileVersion VersionInfo { get; set; }
    }
}
