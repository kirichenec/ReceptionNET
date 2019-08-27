using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reception.App.Network
{
    public interface INetworkServise<T>
    {
        string ServerPath { get; set; }

        Task<IEnumerable<T>> SearchTAsync(string searchText);
    }
}
