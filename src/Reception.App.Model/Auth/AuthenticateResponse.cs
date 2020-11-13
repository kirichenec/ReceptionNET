using Reception.Extensions;

namespace Reception.App.Model.Auth
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
    }

    public static class AuthenticateResponseExtensions
    {
        public static bool IsAuthInfoCorrect(this AuthenticateResponse value)
        {
            return
                value != null
                && value.Id != -1
                && !value.Token.IsNullOrWhiteSpace();

        }
    }
}