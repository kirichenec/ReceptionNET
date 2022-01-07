using System.Configuration;

namespace Reception.App.Service
{
    public class TokenSection : ConfigurationSection
    {
        private const string TokenPropertyName = "token";
        private const string UserIdPropertyName = "userId";

        [ConfigurationProperty(UserIdPropertyName)]
        public int UserId
        {
            get => (int)base[UserIdPropertyName];
            set => base[UserIdPropertyName] = value;
        }

        [ConfigurationProperty(TokenPropertyName)]
        public string Token
        {
            get => (string)base[TokenPropertyName];
            set => base[TokenPropertyName] = value;
        }
    }
}