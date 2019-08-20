using System.Configuration;

namespace Reception.App.Models
{
    public static class AppSettings
    {
        public static bool IsBoss { get { return bool.Parse(ConfigurationManager.AppSettings[nameof(IsBoss)]); } }
        public static string ServerPath { get { return ConfigurationManager.AppSettings[nameof(ServerPath)]; } }
    }
}
