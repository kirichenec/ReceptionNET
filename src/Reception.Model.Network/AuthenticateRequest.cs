using System.ComponentModel.DataAnnotations;

namespace Reception.Model.Network
{
    public class AuthenticateRequest
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}