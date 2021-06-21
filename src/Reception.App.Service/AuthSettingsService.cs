using Reception.App.Service.Interface;
using Reception.Model.Interface;
using System.Configuration;

namespace Reception.App.Service
{
    public partial class SettingsService : ISettingsService
    {
        public IToken Token => ((TokenSection)ConfigurationManager.GetSection("tokenSettings")).ToIToken();
    }
}