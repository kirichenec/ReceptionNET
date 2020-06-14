using Reception.Model.Interfaces;
using Reception.Server.Data.Model;
using System.Threading.Tasks;

namespace Reception.Server.Data.Repository
{
    public interface IDataService : IRepository<Person>
    {
        Task<Post> GetPostAsync(int id);
    }
}