using Microsoft.EntityFrameworkCore;
using Reception.Model.Dto;
using Reception.Server.Data.Mapper;
using Reception.Server.Data.Repository;

namespace Reception.Server.Data.Logic;

public class PersonLogic(IDataService dataService) : IPersonLogic
{
    private readonly IDataService _dataService = dataService;


    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<PersonDto> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var person = await _dataService.GetAsync(id, cancellationToken);
        return person.Map();
    }

    public async Task<IEnumerable<PersonDto>> GetByIdsAsync(IEnumerable<int> ids,
        CancellationToken cancellationToken = default)
    {
        var queriedValues = await _dataService.Queryable()
            .Where(p => ids.Contains(p.Id))
            .ToListAsync(cancellationToken);
        return queriedValues.Map();
    }

    public Task<PersonDto> SaveAsync(PersonDto value, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<PersonDto>> SearchAsync(string searchText,
        CancellationToken cancellationToken = default)
    {
        var searchedValues = await _dataService.SearchAsync(searchText, cancellationToken);
        return searchedValues.Map();
    }
}
