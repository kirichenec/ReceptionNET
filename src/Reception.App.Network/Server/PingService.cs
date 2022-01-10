using Reception.App.Service.Interface;
using Splat;
using System.Threading.Tasks;

namespace Reception.App.Network.Server
{
    public class PingService : IPingService
    {
        private readonly ISettingsService _settingsService;

        public PingService()
        {
            _settingsService = Locator.Current.GetService<ISettingsService>();
        }

        public string ServerPath => _settingsService.DataServerPath;

        public async Task<string> PingAsync()
        {
            var response = await Core.ExecuteGetTaskAsync(
                baseUrl: $"{ServerPath}/api",
                methodUri: "Helper/Ping");
            return response.Content;
        }
    }
}