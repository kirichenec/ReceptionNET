using Newtonsoft.Json;
using Reception.Model.Interface;

namespace Reception.Model.Dto
{
    public class UserDto : IUnique
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string MiddleName { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
    }
}