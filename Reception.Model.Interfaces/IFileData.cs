namespace Reception.Model.Interfaces
{
    public interface IFileData : IUnique
    {
        public byte[] Value { get; set; }

        public IFileVersionInfo VersionInfo { get; set; }
    }
}