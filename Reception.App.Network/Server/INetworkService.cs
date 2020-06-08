using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reception.App.Network.Server
{
    public interface INetworkService<T>
    {
        Task<IEnumerable<T>> SearchAsync(string searchText);
    }
}