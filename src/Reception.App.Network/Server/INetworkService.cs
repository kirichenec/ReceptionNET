namespace Reception.App.Network.Server
{
    public interface INetworkService<T>
    {
        Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> SearchAsync(string searchText, CancellationToken cancellationToken = default);
    }
}
