using Reception.Model.Interface;

namespace Reception.Model.Network
{
    public class Token : IToken
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Value { get; set; }
    }
}