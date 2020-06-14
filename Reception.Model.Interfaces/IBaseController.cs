using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Reception.Model.Interfaces
{
    public interface IBaseController
    {
        public Task<IActionResult> GetAsync(int id);
        public Task<IActionResult> SearchAsync(string searchText);
    }
}