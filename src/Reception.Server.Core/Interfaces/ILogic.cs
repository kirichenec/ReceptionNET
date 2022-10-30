namespace Reception.Server.Core.Interfaces
{
    public interface ILogic<T>
    {
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<T> GetAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default);
        Task<T> SaveAsync(T value, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> SearchAsync(string searchText, CancellationToken cancellationToken = default);
    }
}