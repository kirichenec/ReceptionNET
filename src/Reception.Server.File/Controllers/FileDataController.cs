using Microsoft.AspNetCore.Mvc;
using Reception.Model.Network;
using Reception.Server.Auth.ConnectionLibrary;
using Reception.Server.Core.Interfaces;
using Reception.Server.File.Logic;
using Reception.Server.File.Model.Dto;

namespace Reception.Server.File.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public class FileDataController : ControllerBase, IBaseController
    {
        private readonly IFileDataLogic _fileDataLogic;

        public FileDataController(IFileDataLogic fileDataLogic)
        {
            _fileDataLogic = fileDataLogic;
        }

        // GET api/FileData/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken = default)
        {
            var fileData = await _fileDataLogic.GetAsync(id, cancellationToken);
            var info = new QueryResult<FileDataDto>(fileData);
            return Ok(info);
        }

        // POST api/FileData
        [HttpPost]
        public async Task<IActionResult> Upload(string fileName, byte[] value,
            CancellationToken cancellationToken = default)
        {
            var savedFileInfo = await _fileDataLogic.SaveAsync(fileName, value, cancellationToken);
            var info = new QueryResult<FileDataDto>(savedFileInfo);
            return Ok(info);
        }

        // DELETE api/FileData/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            var isDeleteSuccess = await _fileDataLogic.DeleteAsync(id, cancellationToken);
            var info = new QueryResult<bool>(isDeleteSuccess);
            return Ok(info);
        }

        // GET api/FileData?searchText=5
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string searchText,
            CancellationToken cancellationToken = default)
        {
            var searchResult = await _fileDataLogic.SearchAsync(searchText, cancellationToken);
            var info = new QueryResult<IEnumerable<FileDataDto>>(searchResult);
            return Ok(info);
        }
    }
}