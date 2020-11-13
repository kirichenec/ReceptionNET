using System.Threading.Tasks;

namespace Reception.App.Network.Server
{
    public class PingService : IPingService
    {
        public PingService(string serverPath)
        {
            ServerPath = serverPath;
        }

        public string ServerPath { get; set; }

        public async Task<string> PingAsync()
        {
            var response = await Core.ExecuteGetTaskAsync($"{ServerPath}/api/Helper/Ping");
            return response.Content;
        }
    }
}
