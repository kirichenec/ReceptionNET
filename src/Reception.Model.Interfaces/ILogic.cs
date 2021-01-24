using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reception.Model.Interface
{
    public interface ILogic<T>
    {
        Task<bool> DeleteAsync(int id);
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetByIdsAsync(IEnumerable<int> ids);
        Task<T> SaveAsync(T value);
        Task<IEnumerable<T>> SearchAsync(string searchText);
    }
}