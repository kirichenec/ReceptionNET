using System.Configuration;

namespace Reception.App.Models
{
    public static class AppSettings
    {
        public static string ChatServerPath => ConfigurationManager.AppSettings[nameof(ChatServerPath)];
        public static string DataServerPath => ConfigurationManager.AppSettings[nameof(DataServerPath)];
        public static string FileServerPath => ConfigurationManager.AppSettings[nameof(FileServerPath)];
        public static bool IsBoss => bool.Parse(ConfigurationManager.AppSettings[nameof(IsBoss)]);
        public static int PingDelay => int.Parse(ConfigurationManager.AppSettings[nameof(PingDelay)]);
        public static string UserServerPath => ConfigurationManager.AppSettings[nameof(UserServerPath)];
        public static string WelcomeMessage => ConfigurationManager.AppSettings[nameof(WelcomeMessage)];
    }
}