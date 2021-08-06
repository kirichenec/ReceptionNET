using System.Configuration;

namespace Reception.App.Service
{
    public class TokenSection : ConfigurationSection
    {
        [ConfigurationProperty("userId")]
        public int UserId
        {
            get => (int)base["userId"];
            set => base["userId"] = value;
        }

        [ConfigurationProperty("token")]
        public string Token
        {
            get => (string)base["token"];
            set => base["token"] = value;
        }
    }
}