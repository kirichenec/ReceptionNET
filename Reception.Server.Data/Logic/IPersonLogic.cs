using Reception.Server.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reception.Server.Data.Logic
{
    public interface IPersonLogic
    {
        Task<Person> GetPersonAsync(int id);
        Task<List<Person>> SearchPersonAsync(string searchText);
    }
}