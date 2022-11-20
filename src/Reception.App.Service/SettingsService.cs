using Reception.App.Model.Auth;
using Reception.App.Service.Interface;
using Reception.Extension;
using static Reception.Extension.ConfigurationManagerExtensions;

namespace Reception.App.Service
{
    public partial class SettingsService : ISettingsService
    {
        private const string TOKEN_SECTION_NAME = "tokenSettings";

        public string AuthServerPath
        {
            get => GetAppSettingsParam();
            set => value.SetAppSettingsParam();
        }

        public string ChatServerPath
        {
            get => GetAppSettingsParam();
            set => value.SetAppSettingsParam();
        }

        public string DataServerPath
        {
            get => GetAppSettingsParam();
            set => value.SetAppSettingsParam();
        }

        public string DefaultVisitorPhotoPath
        {
            get => GetAppSettingsParam();
            set => value.SetAppSettingsParam();
        }

        public string FileServerPath
        {
            get => GetAppSettingsParam();
            set => value.SetAppSettingsParam();
        }

        public bool IsBoss
        {
            get => GetAppSettingsParam().ParseBool();
            set => value.SetAppSettingsParam();
        }

        public bool IsDark
        {
            get => GetAppSettingsParam().ParseBool();
            set => value.SetAppSettingsParam();
        }

        public int PingDelay
        {
            get => GetAppSettingsParam().ParseInt();
            set => value.SetAppSettingsParam();
        }

        public Token Token
        {
            get => TOKEN_SECTION_NAME.GetSection<TokenSection>().ToToken();
            set => value.CreateTokenSection().UpdateSection(TOKEN_SECTION_NAME);
        }

        public string WelcomeMessage
        {
            get => GetAppSettingsParam();
            set => value.SetAppSettingsParam();
        }

        public bool WithReconnect
        {
            get => GetAppSettingsParam().ParseBool();
            set => value.SetAppSettingsParam();
        }
    }
}
