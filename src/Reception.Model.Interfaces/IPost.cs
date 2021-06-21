namespace Reception.Model.Interface
{
    public interface IPost : IUnique, ICommentable
    {
        string Name { get; set; }
    }
}