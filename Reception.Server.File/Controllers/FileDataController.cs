using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reception.Model.Interfaces;
using Reception.Model.Network;
using Reception.Server.File.Logic;
using System.Threading.Tasks;

namespace Reception.Server.File.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileDataController : ControllerBase
    {
        private readonly IFileDataLogic _fileDataLogic;

        public FileDataController(IFileDataLogic fileDataLogic)
        {
            _fileDataLogic = fileDataLogic;
        }

        [HttpGet("Data/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var fileData = await _fileDataLogic.GetFileAsync(id);
            var info = new QueryResult<IFormFile>(fileData);
            return Ok(info);
        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadAsync(IFormFile value)
        {
            var savedFileInfo = await _fileDataLogic.SaveAsync(value);
            var info = new QueryResult<IFileVersionInfo>(savedFileInfo);
            return Ok(info);
        }

        [HttpDelete("{id}")]
        [Route(nameof(DeleteAsync))]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var isDeleteSuccess = await _fileDataLogic.DeleteFileAsync(id);
            var info = new QueryResult<bool>(isDeleteSuccess);
            return Ok(info);
        }
    }
}