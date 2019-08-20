using Reception.Model.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reception.Server.Repository
{
    public interface IDataService
    {
        Task<PersonDto> GetPersonAsync(int uid);
        Task<PostDto> GetPostAsync(int uid);
        IQueryable<PersonDto> QueryablePersons();
        Task<List<PersonDto>> SearchPersonsAsync(string searchText);
        Task<List<PersonDto>> SearchPersonsPagedAsync(string searchText, int count, int page);
    }
}