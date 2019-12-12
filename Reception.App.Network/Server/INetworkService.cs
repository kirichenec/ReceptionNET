using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reception.App.Network.Server
{
    public interface INetworkService<T>
    {
        string ServerPath { get; set; }

        Task<IEnumerable<T>> SearchTAsync(string searchText);
    }
}