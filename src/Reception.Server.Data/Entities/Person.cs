using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reception.Server.Data.Entities
{
    [Table("Person", Schema = "Person")]
    public class Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public PersonAdditional AdditionalInfo { get; set; }

        public string Comment { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public Post Post { get; set; }

        [Required]
        public string SecondName { get; set; }
    }
}