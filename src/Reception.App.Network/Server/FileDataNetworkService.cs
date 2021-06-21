using Reception.App.Model.FileInfo;
using Reception.App.Service.Interface;
using Splat;

namespace Reception.App.Network.Server
{
    public class FileDataNetworkService : NetworkService<FileData>
    {
        public FileDataNetworkService() : base(Locator.Current.GetService<ISettingsService>().FileServerPath)
        {
        }
    }
}