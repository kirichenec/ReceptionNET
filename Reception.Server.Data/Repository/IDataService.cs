using Reception.Server.Data.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reception.Server.Data.Repository
{
    public interface IDataService
    {
        Task<Person> GetPersonAsync(int id);
        Task<Post> GetPostAsync(int id);
        IQueryable<Person> QueryablePersons();
        Task<List<Person>> SearchPersonsAsync(string searchText);
        Task<List<Person>> SearchPersonsPagedAsync(string searchText, int count, int page);
    }
}