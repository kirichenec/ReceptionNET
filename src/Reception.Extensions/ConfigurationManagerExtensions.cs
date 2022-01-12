using System.Configuration;

namespace Reception.Extension
{
    public static class ConfigurationManagerExtensions
    {
        public static T GetSection<T>(this string sectionName)
        {
            return (T)ConfigurationManager.GetSection(sectionName);
        }

        public static void UpdateSection(this ConfigurationSection value, string sectionName)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.Sections.Remove(sectionName);
            config.Sections.Add(sectionName, value);
            config.Save(ConfigurationSaveMode.Modified);
        }
    }
}