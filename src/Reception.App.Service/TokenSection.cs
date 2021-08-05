using System.Configuration;

namespace Reception.App.Service
{
    public class TokenSection : ConfigurationSection
    {
        [ConfigurationProperty("userId")]
        public UserIdElement UserId
        {
            get => (UserIdElement)this["userId"];
            set => this["userId"] = value;
        }

        [ConfigurationProperty("token")]
        public TokenElement Token
        {
            get => (TokenElement)this["token"];
            set => this["token"] = value;
        }
    }

    public class UserIdElement : ConfigurationElement
    {
        [ConfigurationProperty("value")]
        public int Value
        {
            get => (int)this["value"];
            set => this["value"] = value;
        }
    }

    public class TokenElement : ConfigurationElement
    {
        [ConfigurationProperty("value")]
        public string Value
        {
            get => (string)this["value"];
            set => this["value"] = value;
        }
    }
}