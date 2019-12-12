using System.Threading.Tasks;

namespace Reception.App.Network.Server
{
    public interface IPingService
    {
        string ServerPath { get; set; }

        Task<string> PingAsync();
    }
}