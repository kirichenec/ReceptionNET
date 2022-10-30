namespace Reception.Server.Core.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> GetAsync(int id, CancellationToken cancellationToken = default);
        IQueryable<T> Queryable();
        Task<IEnumerable<T>> SearchAsync(string searchText, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> SearchPagedAsync(string searchText, int count, int page, CancellationToken cancellationToken = default);
    }
}