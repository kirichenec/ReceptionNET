using Reception.Model.Interface;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reception.Server.File.Model
{
    [Table("FileVersion", Schema = "Files")]
    public class FileVersion : IFileVersion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Comment { get; set; }

        public string Extension { get; set; }

        [NotMapped]
        public string FullName => $"{Name}.{Extension}";

        [Required]
        public string Name { get; set; }

        [Required]
        public Guid Version { get; set; }
    }
}