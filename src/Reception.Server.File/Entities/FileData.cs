using Reception.Model.Interface;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Reception.Model.Interface.IFileData;

namespace Reception.Server.File.Entities
{
    [Table("FileData", Schema = "Files")]
    public class FileData : IFileData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Additional { get; set; }

        public string Comment { get; set; }

        [Required]
        public byte[] Data { get; set; }

        public string Extension { get; set; }

        [NotMapped]
        public string FullName => this.GetFullName();

        public string Name { get; set; }

        public FileType Type { get; set; }

        public Guid Version { get; set; }
    }
}