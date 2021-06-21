using Reception.Model.Interface;

namespace Reception.App.Model.Auth
{
    public class Token : IToken
    {
        public int UserId { get; set; }

        public string Value { get; set; }
    }
}