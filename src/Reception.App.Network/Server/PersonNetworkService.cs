using Reception.App.Model.PersonInfo;
using Reception.App.Network.Auth;
using Reception.App.Service.Interface;

namespace Reception.App.Network.Server
{
    public class PersonNetworkService : NetworkService<Person>, IPersonNetworkService
    {
        public PersonNetworkService(ISettingsService settingsService, IAuthService authService)
            : base(settingsService.DataServerPath, authService)
        {
        }
    }
}