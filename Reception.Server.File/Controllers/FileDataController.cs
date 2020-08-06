using Microsoft.AspNetCore.Mvc;
using Reception.Model.Interfaces;
using Reception.Model.Network;
using Reception.Server.File.Logic;
using Reception.Server.File.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reception.Server.File.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileDataController : ControllerBase, IBaseController
    {
        private readonly IFileDataLogic _fileDataLogic;

        public FileDataController(IFileDataLogic fileDataLogic)
        {
            _fileDataLogic = fileDataLogic;
        }

        // GET api/FileData/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get(int id)
        {
            var fileData = await _fileDataLogic.GetAsync(id);
            var info = new QueryResult<FileDataDto>(fileData);
            return Ok(info);
        }

        // POST api/FileData
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UploadAsync(string fileName, byte[] value)
        {
            var savedFileInfo = await _fileDataLogic.SaveAsync(fileName, value);
            var info = new QueryResult<FileDataDto>(savedFileInfo);
            return Ok(info);
        }

        // DELETE api/FileData/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var isDeleteSuccess = await _fileDataLogic.DeleteAsync(id);
            var info = new QueryResult<bool>(isDeleteSuccess);
            return Ok(info);
        }

        // GET api/FileData?searchText=5
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Search([FromQuery] string searchText)
        {
            var searchResult = await _fileDataLogic.SearchAsync(searchText);
            var info = new QueryResult<IEnumerable<FileDataDto>>(searchResult);
            return Ok(info);
        }
    }
}