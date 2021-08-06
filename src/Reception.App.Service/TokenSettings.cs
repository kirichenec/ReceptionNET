using Reception.Model.Interface;

namespace Reception.App.Service
{
    public class TokenSettings : IToken
    {
        public int UserId { get; set; }
        public string Value { get; set; }
    }
}