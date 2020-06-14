using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reception.Model.Interfaces
{
    public interface ILogic<T>
    {
        Task<bool> DeleteAsync(int id);
        Task<T> GetAsync(int id);
        Task<T> SaveAsync(T value);
        Task<List<T>> SearchAsync(string searchText);
    }
}