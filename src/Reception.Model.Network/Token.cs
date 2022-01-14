using Reception.Model.Interface;

namespace Reception.Model.Network
{
    public class Token : IToken
    {
        public int UserId { get; set; }

        public string Value { get; set; }
    }
}