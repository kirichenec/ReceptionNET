using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reception.App.Network.Server
{
    public interface INetworkService<T>
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetByIds(IEnumerable<int> ids);
        Task<IEnumerable<T>> SearchAsync(string searchText);
    }
}