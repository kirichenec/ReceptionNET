using Reception.App.Model.PersonInfo;
using Reception.App.Service.Interface;
using Splat;

namespace Reception.App.Network.Server
{
    public class PersonNetworkService : NetworkService<Person>
    {
        public PersonNetworkService() : base(Locator.Current.GetService<ISettingsService>().DataServerPath)
        {
        }
    }
}