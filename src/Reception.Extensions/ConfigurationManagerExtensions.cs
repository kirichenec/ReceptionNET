using System.Configuration;
using System.Runtime.CompilerServices;

namespace Reception.Extension
{
    public static class ConfigurationManagerExtensions
    {
        public static string GetAppSettingsParam([CallerMemberName] string parameterName = null)
        {
            return ConfigurationManager.AppSettings[parameterName];
        }

        public static T GetSection<T>(this string sectionName)
        {
            return (T)ConfigurationManager.GetSection(sectionName);
        }

        public static void SetAppSettingsParam<T>(this T value, [CallerMemberName] string parameterName = null)
        {
            var config = GetConfiguration();
            config.AppSettings.Settings[parameterName].Value = value.ToString();
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public static void UpdateSection(this ConfigurationSection value, string sectionName)
        {
            var config = GetConfiguration();
            config.Sections.Remove(sectionName);
            config.Sections.Add(sectionName, value);
            config.Save(ConfigurationSaveMode.Modified);
        }

        private static Configuration GetConfiguration()
        {
            return ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }
    }
}