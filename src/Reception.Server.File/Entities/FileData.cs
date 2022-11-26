using Reception.Model.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reception.Server.File.Entities
{
    [Table("FileData", Schema = "Files")]
    public class FileData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Comment { get; set; }

        [Required]
        public byte[] Data { get; set; }

        public string Extension { get; set; }

        [Required]
        public string Name { get; set; }

        public FileType Type { get; set; }

        public Guid Version { get; set; }
    }
}