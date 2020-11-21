using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reception.Server.Auth.Entities
{
    [Table("User", Schema = "Auth")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        public string Login { get; set; }

        public string MiddleName { get; set; }

        public string Password { get; set; }
    }
}