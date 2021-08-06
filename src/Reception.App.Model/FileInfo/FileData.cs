using Reception.Model.Interface;
using System;
using static Reception.Model.Interface.IFileData;

namespace Reception.App.Model.FileInfo
{
    public class FileData : IFileData
    {
        public string Comment { get; set; }

        public byte[] Data { get; set; }

        public string Extension { get; set; }

        public string FullName => this.GetFullName();

        public int Id { get; set; }

        public string Name { get; set; }

        public FileType Type { get; set; }

        public Guid Version { get; set; }
    }
}