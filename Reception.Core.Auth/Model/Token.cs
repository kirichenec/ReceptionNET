using Reception.Model.Interface;

namespace Reception.Core.Auth.Model
{
    public class Token : IToken
    {
        public virtual int Id { get; set; }

        public virtual int UserId { get; set; }

        public virtual string Value { get; set; }
    }
}