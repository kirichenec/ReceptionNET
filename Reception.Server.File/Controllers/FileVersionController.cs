using Microsoft.AspNetCore.Mvc;
using Reception.Model.Interfaces;
using Reception.Model.Network;
using Reception.Server.Data.Extensions;
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
        public async Task<IActionResult> GetAsync(int id)
        {
            var fileVersion = await _fileVersionLogic.GetAsync(id);
            var info = new QueryResult<FileVersionDto>(fileVersion.ToDto());
            return Ok(info);
        }

        [HttpGet]
        public async Task<IActionResult> SearchAsync(string searchText)
        {
            var result = await _fileVersionLogic.SearchAsync(searchText);
            var info = new QueryResult<List<FileVersionDto>>(result.ToDto());
            return Ok(info);
        }
    }
}