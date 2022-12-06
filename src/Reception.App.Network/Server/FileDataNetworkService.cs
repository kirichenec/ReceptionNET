using Reception.App.Model.FileInfo;
using Reception.App.Network.Auth;
using Reception.App.Service.Interface;

namespace Reception.App.Network.Server
{
    public class FileDataNetworkService : NetworkService<FileData>, IFileDataNetworkService
    {
        public FileDataNetworkService(ISettingsService settingsService, IAuthService authService)
            : base(settingsService.FileServerPath, authService)
        {
        }
    }
}