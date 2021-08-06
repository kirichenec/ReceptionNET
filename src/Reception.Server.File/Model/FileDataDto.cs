using Reception.Model.Interface;
using System;
using static Reception.Model.Interface.IFileData;

namespace Reception.Server.File.Model.Dto
{
    public class FileDataDto : IUnique
    {
        public string Additional { get; set; }

        public string Comment { get; set; }

        public byte[] Data { get; set; }

        public string Extension { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public FileType Type { get; set; }

        public Guid Version { get; set; }
    }
}
