using Microsoft.AspNetCore.Mvc;

namespace Reception.Server.Core.Interfaces
{
    public interface IBaseController
    {
        public Task<IActionResult> Get(int id);
        public Task<IActionResult> Search(string searchText);
    }
}