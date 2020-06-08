namespace Reception.Model.Interfaces
{
    public interface IPost : IUnique, ICommentable
    {
        string Name { get; set; }
    }
}