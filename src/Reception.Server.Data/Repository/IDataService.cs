using Reception.Server.Core.Interfaces;
using Reception.Server.Data.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Reception.Server.Data.Repository
{
    public interface IDataService : IRepository<Person>
    {
        Task<Post> GetPostAsync(int id, CancellationToken cancellationToken = default);
    }
}