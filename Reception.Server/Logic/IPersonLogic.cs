using Reception.Server.Model;
using System.Threading.Tasks;

namespace Reception.Server.Logic
{
    public interface IPersonLogic
    {
        Task<Person> GetPersonAsync(int uid);
    }
}
