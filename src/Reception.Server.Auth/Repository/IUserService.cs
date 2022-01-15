using Reception.Server.Auth.Entities;
using Reception.Server.Core.Interfaces;
using System.Threading.Tasks;

namespace Reception.Server.Auth.Repository
{
    public interface IUserService : IRepository<User>
    {
        Task<bool> DeleteAsync(int id);
        Task<User> SaveAsync(User value);
    }
}