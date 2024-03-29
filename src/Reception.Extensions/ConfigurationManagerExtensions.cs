﻿using System.Configuration;
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

        public static void UpdateAppSettingsParam<T>(this T value, [CallerMemberName] string parameterName = null)
        {
            UpdateSectionInternal(value, parameterName, "appSettings", UpdateOrCreateSection);


            static void UpdateOrCreateSection(T value, string parameterName, Configuration config)
            {
                var stringValue = value.ToString();

                if (config.AppSettings.Settings[parameterName] is KeyValueConfigurationElement setting)
                {
                    setting.Value = stringValue;
                }
                else
                {
                    config.AppSettings.Settings.Add(new KeyValueConfigurationElement(parameterName, stringValue));
                }
            }
        }

        public static void UpdateSection(this ConfigurationSection value, string sectionName)
        {
            UpdateSectionInternal(value, sectionName, sectionName, UpdateSection);


            static void UpdateSection(ConfigurationSection value, string sectionName, Configuration config)
            {
                config.Sections.Remove(sectionName);
                config.Sections.Add(sectionName, value);
            }
        }

        private static void UpdateSectionInternal<Tin>(Tin value, string parameterName, string sectionName,
            Action<Tin, string, Configuration> updateAction)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            updateAction(value, parameterName, config);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(sectionName);
        }
    }
}