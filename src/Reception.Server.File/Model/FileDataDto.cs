using Reception.Model.Interfaces;

namespace Reception.Server.File.Model.Dto
{
    public class FileDataDto : IUnique
    {
        public int Id { get; set; }

        public byte[] Value { get; set; }

        public IFileVersion VersionInfo { get; set; }
    }
}
