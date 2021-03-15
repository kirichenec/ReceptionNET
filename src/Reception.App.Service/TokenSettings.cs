using Reception.Model.Interface;
using System.Configuration;

namespace Reception.App.Service
{
    public class TokenSettings : IToken
    {
        public int UserId { get; set; }
        public string Value { get; set; }
    }

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

        public IToken ToIToken()
        {
            return new TokenSettings
            {
                UserId = UserId?.Value ?? 0,
                Value = Token?.Value
            };
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