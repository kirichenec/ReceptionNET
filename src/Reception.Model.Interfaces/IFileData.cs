namespace Reception.Model.Interface
{
    public interface IFileData : IUnique
    {
        public byte[] Value { get; set; }

        public IFileVersion VersionInfo { get; set; }
    }
}