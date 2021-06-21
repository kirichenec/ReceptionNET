using System.ComponentModel.DataAnnotations;

namespace Reception.App.Model.Auth
{
    public class AuthenticateRequest
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}