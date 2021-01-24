using Microsoft.AspNetCore.Mvc;
using Reception.Model.Interface;
using Reception.Model.Network;
using Reception.Server.File.Logic;
using Reception.Server.File.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reception.Server.File.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileVersionController : ControllerBase, IBaseController
    {
        private readonly IFileVersionLogic _fileVersionLogic;

        public FileVersionController(IFileVersionLogic fileVersionLogic)
        {
            _fileVersionLogic = fileVersionLogic;
        }

        // GET api/<FileVersionInfoController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get(int id)
        {
            var fileVersion = await _fileVersionLogic.GetAsync(id);
            var info = new QueryResult<FileVersionDto>(fileVersion);
            return Ok(info);
        }

        // GET api/FileVersion?searchText=5
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Search(string searchText)
        {
            var result = await _fileVersionLogic.SearchAsync(searchText);
            var info = new QueryResult<IEnumerable<FileVersionDto>>(result);
            return Ok(info);
        }
    }
}