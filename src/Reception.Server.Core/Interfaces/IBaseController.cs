using Microsoft.AspNetCore.Mvc;

namespace Reception.Server.Core.Interfaces
{
    public interface IBaseController
    {
        public Task<IActionResult> Get(int id, CancellationToken cancellationToken = default);
        public Task<IActionResult> Search(string searchText, CancellationToken cancellationToken = default);
    }
}