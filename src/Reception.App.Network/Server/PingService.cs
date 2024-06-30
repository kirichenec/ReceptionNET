using Reception.App.Service.Interface;

namespace Reception.App.Network.Server
{
    public class PingService(ISettingsService settingsService) : IPingService
    {
        private readonly ISettingsService _settingsService = settingsService;


        public string ServerPath => _settingsService.DataServerPath;


        public async Task<string> PingAsync()
        {
            var response = await Core.ExecuteGetTaskAsync(
                baseUrl: $"{ServerPath}/api",
                methodUri: "Ping");
            return response.Content;
        }
    }
}
