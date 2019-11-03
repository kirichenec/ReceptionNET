using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Reception.Server.Controllers
{
    public class HelperController : Controller
    {
        // GET api/Helper/ping
        [HttpGet("/ping")]
        public async Task<IActionResult> Ping()
        {
            return await Task.FromResult<IActionResult>(Ok());
        }
    }
}