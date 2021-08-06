using Reception.App.Service.Interface;
using Reception.Extension;
using Reception.Model.Interface;

namespace Reception.App.Service
{
    public partial class SettingsService : ISettingsService
    {
        private const string TOKEN_SECTION_NAME = "tokenSettings";

        public IToken Token
        {
            get => TOKEN_SECTION_NAME.GetSection<TokenSection>().ToIToken();
            set => value.CreateTokenSection().UpdateSection(TOKEN_SECTION_NAME);
        }
    }
}