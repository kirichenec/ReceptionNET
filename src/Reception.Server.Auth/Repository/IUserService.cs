using Reception.Model.Interface;
using Reception.Server.Auth.Entities;
using System.Threading.Tasks;

namespace Reception.Server.Auth.Repository
{
    public interface IUserService : IRepository<User>
    {
        Task<bool> DeleteAsync(int id);
        Task<User> SaveAsync(User value);
    }
}