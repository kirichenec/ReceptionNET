using Reception.Model.Interface;
using System;

namespace Reception.App.Model.FileInfo
{
    public class FileData
    {
        public string Comment { get; set; }

        public byte[] Data { get; set; }

        public string Extension { get; set; }

        public string FullName => string.Join('.', Name, Extension);

        public int Id { get; set; }

        public string Name { get; set; }

        public FileType Type { get; set; }

        public Guid Version { get; set; }
    }
}