using Reception.App.Service.Interface;
using System.Configuration;

namespace Reception.App.Service
{
    public partial class SettingsService : ISettingsService
    {
        public string ChatServerPath
        {
            get
            {
                return ConfigurationManager.AppSettings[nameof(ChatServerPath)];
            }
            set
            {
                ConfigurationManager.AppSettings[nameof(ChatServerPath)] = value;
            }
        }

        public string DataServerPath
        {
            get
            {
                return ConfigurationManager.AppSettings[nameof(DataServerPath)];
            }
            set
            {
                ConfigurationManager.AppSettings[nameof(DataServerPath)] = value;
            }
        }

        public string FileServerPath
        {
            get
            {
                return ConfigurationManager.AppSettings[nameof(FileServerPath)];
            }
            set
            {
                ConfigurationManager.AppSettings[nameof(FileServerPath)] = value;
            }
        }

        public bool IsBoss
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings[nameof(IsBoss)]);
            }
            set
            {
                ConfigurationManager.AppSettings[nameof(IsBoss)] = value.ToString();
            }
        }

        public int PingDelay
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings[nameof(PingDelay)]);
            }
            set
            {
                ConfigurationManager.AppSettings[nameof(PingDelay)] = value.ToString();
            }
        }

        public string UserServerPath
        {
            get
            {
                return ConfigurationManager.AppSettings[nameof(UserServerPath)];
            }
            set
            {
                ConfigurationManager.AppSettings[nameof(UserServerPath)] = value;
            }
        }

        public string WelcomeMessage
        {
            get
            {
                return ConfigurationManager.AppSettings[nameof(WelcomeMessage)];
            }
            set
            {
                ConfigurationManager.AppSettings[nameof(WelcomeMessage)] = value;
            }
        }

        public bool WithReconnect
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings[nameof(WithReconnect)]);
            }
            set
            {
                ConfigurationManager.AppSettings[nameof(WithReconnect)] = value.ToString();
            }
        }
    }
}