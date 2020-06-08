using Reception.Model.Interfaces;

namespace Reception.Server.File.Model.Dto
{
    public class DataDto : IUnique
    {
        public int Id { get; set; }

        public byte[] Value { get; set; }

        public IFileVersionInfo VersionInfo { get; set; }
    }
}
