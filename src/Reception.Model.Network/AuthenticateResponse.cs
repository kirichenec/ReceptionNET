namespace Reception.Model.Network
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string MiddleName { get; set; }
        public string Token { get; set; }
    }
}