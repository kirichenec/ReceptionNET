using System.Threading.Tasks;

namespace Reception.App.Network
{
    public interface IPingService
    {
        string ServerPath { get; set; }

        Task<string> PingAsync();
    }
}
