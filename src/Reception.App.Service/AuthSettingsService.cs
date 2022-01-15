using Reception.App.Model.Auth;
using Reception.App.Service.Interface;
using Reception.Extension;

namespace Reception.App.Service
{
    public partial class SettingsService : ISettingsService
    {
        private const string TOKEN_SECTION_NAME = "tokenSettings";

        public Token Token
        {
            get => TOKEN_SECTION_NAME.GetSection<TokenSection>().ToToken();
            set => value.CreateTokenSection().UpdateSection(TOKEN_SECTION_NAME);
        }
    }
}