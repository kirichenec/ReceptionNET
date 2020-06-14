using Microsoft.AspNetCore.Mvc;
using Reception.Model.Interfaces;
using Reception.Model.Network;
using Reception.Server.File.Extensions;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var fileData = await _fileDataLogic.GetAsync(id);
            var info = new QueryResult<FileDataDto>(fileData.ToDto());
            return Ok(info);
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(string fileName, byte[] value)
        {
            var savedFileInfo = await _fileDataLogic.SaveAsync(fileName, value);
            var info = new QueryResult<FileDataDto>(savedFileInfo.ToDto());
            return Ok(info);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var isDeleteSuccess = await _fileDataLogic.DeleteAsync(id);
            var info = new QueryResult<bool>(isDeleteSuccess);
            return Ok(info);
        }

        [HttpGet]
        public async Task<IActionResult> SearchAsync([FromQuery] string searchText)
        {
            var searchResult = await _fileDataLogic.SearchAsync(searchText);
            var info = new QueryResult<List<FileDataDto>>(searchResult.ToDto());
            return Ok(info);
        }
    }
}