using Reception.App.Service.Interface;

namespace Reception.App.Network.Server
{
    public class PingService : IPingService
    {
        private readonly ISettingsService _settingsService;

        public PingService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

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