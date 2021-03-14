using Reception.App.Service.Interface;
using System.Configuration;

namespace Reception.App.Service
{
    public class SettingsService : ISettingsService
    {
        public string ChatServerPath => ConfigurationManager.AppSettings[nameof(ChatServerPath)];
        public string DataServerPath => ConfigurationManager.AppSettings[nameof(DataServerPath)];
        public string FileServerPath => ConfigurationManager.AppSettings[nameof(FileServerPath)];
        public bool IsBoss => bool.Parse(ConfigurationManager.AppSettings[nameof(IsBoss)]);
        public int PingDelay => int.Parse(ConfigurationManager.AppSettings[nameof(PingDelay)]);
        public string UserServerPath => ConfigurationManager.AppSettings[nameof(UserServerPath)];
        public string WelcomeMessage => ConfigurationManager.AppSettings[nameof(WelcomeMessage)];
        public bool WithReconnect => bool.Parse(ConfigurationManager.AppSettings[nameof(WithReconnect)]);
    }
}