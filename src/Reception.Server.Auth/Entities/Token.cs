using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoreToken = Reception.Core.Auth.Model.Token;

namespace Reception.Server.Auth.Entities
{
    [Table("Token", Schema = "Auth")]
    public class Token : CoreToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        [Required]
        public override int UserId { get; set; }

        [Required]
        public override string Value { get; set; }
    }
}