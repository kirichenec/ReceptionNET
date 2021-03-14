namespace Reception.Model.Interface
{
    public interface IToken
    {
        public int UserId { get; set; }

        public string Value { get; set; }
    }
}