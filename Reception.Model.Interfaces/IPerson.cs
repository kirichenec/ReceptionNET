namespace Reception.Model.Interfaces
{
    public interface IPerson<TPost> : IUnique, ICommentable where TPost : class, IPost
    {
        string FirstName { get; set; }

        string MiddleName { get; set; }

        TPost Post { get; set; }

        string SecondName { get; set; }
    }
}