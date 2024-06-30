using Reception.App.Model.FileInfo;
using Reception.App.Network.Auth;
using Reception.App.Service.Interface;

namespace Reception.App.Network.Server
{
    public class FileDataNetworkService(ISettingsService settingsService, IAuthService authService)
        : NetworkService<FileData>(settingsService.FileServerPath, authService), IFileDataNetworkService
    {
    }
}
