using Microsoft.EntityFrameworkCore;
using Reception.Model.Dto;
using Reception.Server.Data.Extensions;
using Reception.Server.Data.Repository;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<PersonDto> GetAsync(int id)
        {
            var person = await _dataService.GetAsync(id);
            return person.ToDto();
        }

        public async Task<IEnumerable<PersonDto>> GetByIdsAsync(IEnumerable<int> ids)
        {
            var queriedValues = await _dataService.Queryable().Where(p => ids.Contains(p.Id)).ToListAsync();
            return queriedValues.ToDtos();
        }

        public Task<PersonDto> SaveAsync(PersonDto value)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<PersonDto>> SearchAsync(string searchText)
        {
            var searchedValues = await _dataService.SearchAsync(searchText);
            return searchedValues.ToDtos();
        }
    }
}