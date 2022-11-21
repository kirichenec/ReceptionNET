using Reception.Server.Core.Interfaces;
using Reception.Server.Data.Entities;

namespace Reception.Server.Data.Repository
{
    public interface IDataService : IRepository<Person>
    {
        Task<Post> GetPostAsync(int id, CancellationToken cancellationToken = default);
    }
}