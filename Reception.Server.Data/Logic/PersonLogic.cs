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

        public Task<bool> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Person> GetAsync(int id)
        {
            return await _dataService.GetAsync(id);
        }

        public Task<Person> SaveAsync(Person value)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Person>> SearchAsync(string searchText)
        {
            return await _dataService.SearchAsync(searchText);
        }
    }
}