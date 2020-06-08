using Reception.Server.Data.Model;
using Reception.Server.Data.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reception.Server.Data.Logic
{
    public class PersonLogic : IPersonLogic
    {
        private readonly IDataService _dataService;

        public PersonLogic(IDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<Person> GetPersonAsync(int id)
        {
            return await _dataService.GetPersonAsync(id);
        }

        public async Task<List<Person>> SearchPersonAsync(string searchText)
        {
            return await _dataService.SearchPersonsAsync(searchText);
        }
    }
}