using Reception.Model.Interface;

namespace Reception.Server.File.Entities
{
    public class FileData
    {
        public int Id { get; set; }

        public string Comment { get; set; }

        public byte[] Data { get; set; }

        public string Extension { get; set; }

        public string Name { get; set; }

        public FileType Type { get; set; }

        public Guid Version { get; set; }
    }
}
