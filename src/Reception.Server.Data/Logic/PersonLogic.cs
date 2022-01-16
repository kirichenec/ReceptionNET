using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Reception.Model.Dto;
using Reception.Server.Data.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reception.Server.Data.Logic
{
    public class PersonLogic : IPersonLogic
    {
        private readonly IDataService _dataService;
        private readonly IMapper _mapper;

        public PersonLogic(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<PersonDto> GetAsync(int id)
        {
            var person = await _dataService.GetAsync(id);
            return _mapper.Map<PersonDto>(person);
        }

        public async Task<IEnumerable<PersonDto>> GetByIdsAsync(IEnumerable<int> ids)
        {
            var queriedValues = await _dataService.Queryable().Where(p => ids.Contains(p.Id)).ToListAsync();
            return _mapper.Map<IEnumerable<PersonDto>>(queriedValues);
        }

        public Task<PersonDto> SaveAsync(PersonDto value)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<PersonDto>> SearchAsync(string searchText)
        {
            var searchedValues = await _dataService.SearchAsync(searchText);
            return _mapper.Map<IEnumerable<PersonDto>>(searchedValues);
        }
    }
}