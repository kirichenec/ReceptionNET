using Reception.Model.Interfaces;

namespace Reception.App.Model.FileInfo
{
    public class FileData : IFileData
    {
        public int Id { get; set; }
        public byte[] Value { get; set; }
        public IFileVersion VersionInfo { get; set; }
    }
}