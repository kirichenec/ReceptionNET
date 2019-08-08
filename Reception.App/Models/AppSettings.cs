using System.Configuration;

namespace Reception.App.Models
{
    public static class AppSettings
    {
        public static bool IsBoss { get { return bool.Parse(ConfigurationManager.AppSettings[nameof(IsBoss)]); } }
    }
}
