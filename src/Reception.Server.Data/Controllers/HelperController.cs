using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Reception.Server.Data.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class HelperController : Controller
    {
        // GET api/Helper/Ping
        [HttpGet("Ping")]
        public async Task<IActionResult> Ping()
        {
            return await Task.FromResult<IActionResult>(Ok());
        }
    }
}