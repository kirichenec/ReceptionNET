namespace Reception.Core.Auth.Model
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string MiddleName { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(UserDto user, string token)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Login = user.Login;
            MiddleName = user.MiddleName;
            Token = token;
        }
    }
}