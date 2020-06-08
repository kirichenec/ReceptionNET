using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reception.Server.File.Repository
{
    public interface IService<T>
    {
        Task<T> GetAsync(int id);
        IQueryable<T> Queryable();
        Task<List<T>> SearchAsync(string searchText);
        Task<List<T>> SearchPagedAsync(string searchText, int count, int page);
    }
}