using Reception.Model.Interfaces;
using System;

namespace Reception.Server.File.Model.Dto
{
    public class FileVersionDto : IFileVersion
    {
        public int Id { get; set; }

        public string Comment { get; set; }

        public string Extension { get; set; }

        public string Name { get; set; }

        public Guid Version { get; set; }
    }
}