namespace Reception.Model.Interfaces
{
    public interface IFileData : IUnique
    {
        public byte[] Value { get; set; }

        public IFileVersion VersionInfo { get; set; }
    }
}