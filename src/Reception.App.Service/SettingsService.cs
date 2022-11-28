using Reception.App.Model.Auth;
using Reception.App.Service.Interface;
using Reception.Extension;
using static Reception.Extension.ConfigurationManagerExtensions;

namespace Reception.App.Service
{
    public class SettingsService : ISettingsService
    {
        private const string TOKEN_SECTION_NAME = "tokenSettings";

        public string AuthServerPath
        {
            get => GetAppSettingsParam();
            set => value.UpdateAppSettingsParam();
        }

        public string ChatServerPath
        {
            get => GetAppSettingsParam();
            set => value.UpdateAppSettingsParam();
        }

        public string DataServerPath
        {
            get => GetAppSettingsParam();
            set => value.UpdateAppSettingsParam();
        }

        public string DefaultVisitorPhotoPath
        {
            get => GetAppSettingsParam();
            set => value.UpdateAppSettingsParam();
        }

        public string FileServerPath
        {
            get => GetAppSettingsParam();
            set => value.UpdateAppSettingsParam();
        }

        public bool IsBoss
        {
            get => GetAppSettingsParam().ParseBool();
            set => value.UpdateAppSettingsParam();
        }

        public bool IsDark
        {
            get => GetAppSettingsParam().ParseBool();
            set => value.UpdateAppSettingsParam();
        }

        public int PingDelay
        {
            get => GetAppSettingsParam().ParseInt();
            set => value.UpdateAppSettingsParam();
        }

        public Token Token
        {
            get => TOKEN_SECTION_NAME.GetSection<TokenSection>().ToToken();
            set => value.CreateTokenSection().UpdateSection(TOKEN_SECTION_NAME);
        }

        public string WelcomeMessage
        {
            get => GetAppSettingsParam();
            set => value.UpdateAppSettingsParam();
        }

        public bool WithReconnect
        {
            get => GetAppSettingsParam().ParseBool();
            set => value.UpdateAppSettingsParam();
        }
    }
}
