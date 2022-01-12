using Reception.App.Service.Interface;
using Reception.Extension;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace Reception.App.Service
{
    public partial class SettingsService : ISettingsService
    {
        public string ChatServerPath
        {
            get => GetAppSettingsParam();
            set => SetAppSettingsParam(value);
        }

        public string DataServerPath
        {
            get => GetAppSettingsParam();
            set => SetAppSettingsParam(value);
        }

        public string DefaultVisitorPhotoPath
        {
            get => GetAppSettingsParam();
            set => SetAppSettingsParam(value);
        }

        public string FileServerPath
        {
            get => GetAppSettingsParam();
            set => SetAppSettingsParam(value);
        }

        public bool IsBoss
        {
            get => GetAppSettingsParam().ParseBool();
            set => SetAppSettingsParam(value);
        }

        public int PingDelay
        {
            get => GetAppSettingsParam().ParseInt();
            set => SetAppSettingsParam(value);
        }

        public string UserServerPath
        {
            get => GetAppSettingsParam();
            set => SetAppSettingsParam(value);
        }

        public string WelcomeMessage
        {
            get => GetAppSettingsParam();
            set => SetAppSettingsParam(value);
        }

        public bool WithReconnect
        {
            get => GetAppSettingsParam().ParseBool();
            set => SetAppSettingsParam(value);
        }

        private static string GetAppSettingsParam([CallerMemberName] string parameterName = null)
        {
            return ConfigurationManager.AppSettings[parameterName];
        }

        private static void SetAppSettingsParam<T>(T value, [CallerMemberName] string parameterName = null)
        {
            ConfigurationManager.AppSettings[parameterName] = value.ToString();
        }
    }
}