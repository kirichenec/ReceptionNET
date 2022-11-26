using Reception.Server.Auth.Entities;
using Reception.Server.Core.Interfaces;

namespace Reception.Server.Auth.Repository
{
    public interface IUserService : IRepository<User>
    {
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<User> SaveAsync(User value, CancellationToken cancellationToken = default);
    }
}