using Reception.App.Model.PersonInfo;
using Reception.App.Network.Auth;
using Reception.App.Service.Interface;

namespace Reception.App.Network.Server
{
    public class PersonNetworkService(ISettingsService settingsService, IAuthService authService)
        : NetworkService<Person>(settingsService.DataServerPath, authService), IPersonNetworkService
    {
    }
}
