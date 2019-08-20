using Reception.Server.Model;
using Reception.Server.Repository;
using System.Threading.Tasks;

namespace Reception.Server.Logic
{
    public class PersonLogic : IPersonLogic
    {
        private IDataService _dataService;

        public PersonLogic(IDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<Person> GetPersonAsync(int uid)
        {
            return PersonExtension.PersonFromDto(await _dataService.GetPersonAsync(uid));
        }
    }
}
