using Microsoft.AspNetCore.Mvc;
using Reception.Model.Interfaces;
using Reception.Model.Network;
using Reception.Server.File.Logic;
using System.Threading.Tasks;

namespace Reception.Server.File.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileVersionController : ControllerBase
    {
        private readonly IFileVersionLogic _fileVersionLogic;

        public FileVersionController(IFileVersionLogic fileVersionLogic)
        {
            _fileVersionLogic = fileVersionLogic;
        }

        // GET api/<FileVersionInfoController>/5
        [HttpGet("Info/{id}")]
        public async Task<IActionResult> GetInfoAsync(int id)
        {
            var versionInfo = await _fileVersionLogic.GetInfoAsync(id);
            var info = new QueryResult<IFileVersionInfo>(versionInfo);
            return Ok(info);
        }
    }
}
