using System.Configuration;

namespace Reception.App.Models
{
    public static class AppSettings
    {
        public static string ChatServerPath { get { return ConfigurationManager.AppSettings[nameof(ChatServerPath)]; } }
        public static bool IsBoss { get { return bool.Parse(ConfigurationManager.AppSettings[nameof(IsBoss)]); } }
        public static string ServerPath { get { return ConfigurationManager.AppSettings[nameof(ServerPath)]; } }
        public static int PingDelay { get { return int.Parse(ConfigurationManager.AppSettings[nameof(PingDelay)]); } }
        public static string WelcomeMessage { get { return ConfigurationManager.AppSettings[nameof(WelcomeMessage)]; } }
        public static string DefaultImagePath { get { return ConfigurationManager.AppSettings[nameof(DefaultImagePath)]; } }
    }
}