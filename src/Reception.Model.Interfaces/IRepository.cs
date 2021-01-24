using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reception.Model.Interface
{
    public interface IRepository<T>
    {
        Task<T> GetAsync(int id);
        IQueryable<T> Queryable();
        Task<IEnumerable<T>> SearchAsync(string searchText);
        Task<IEnumerable<T>> SearchPagedAsync(string searchText, int count, int page);
    }
}