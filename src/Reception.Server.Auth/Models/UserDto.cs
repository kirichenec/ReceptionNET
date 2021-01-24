using Reception.Model.Interface;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Reception.Server.Auth.Models
{
    public class UserDto : IUnique
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string MiddleName { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
    }
}