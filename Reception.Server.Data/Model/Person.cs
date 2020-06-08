using Reception.Model.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reception.Server.Data.Model
{
    public class Person : IPerson<Post>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Comment { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }
        
        public Post Post { get; set; }

        [Required]
        public string SecondName { get; set; }
    }
}