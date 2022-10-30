using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Reception.Server.Core.Controllers
{
    [ApiController]
    [Route("api")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [AllowAnonymous]
    public class PingController : Controller
    {
        // GET api/Ping
        [HttpGet("Ping")]
        public IActionResult Ping()
        {
            return Ok();
        }
    }
}