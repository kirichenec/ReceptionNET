using Reception.Server.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reception.Server.Logic
{
    public interface IPersonLogic
    {
        Task<Person> GetPersonAsync(int id);
        Task<List<Person>> SearchPersonAsync(string searchText);
    }
}