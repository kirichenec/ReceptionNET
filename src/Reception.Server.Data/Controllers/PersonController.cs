using Microsoft.AspNetCore.Mvc;
using Reception.Core.Auth.Helpers;
using Reception.Model.Dto;
using Reception.Model.Interface;
using Reception.Model.Network;
using Reception.Server.Data.Logic;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reception.Server.Data.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase, IBaseController
    {
        private readonly IPersonLogic _personLogic;

        public PersonController(IPersonLogic personLogic)
        {
            _personLogic = personLogic;
        }

        // GET api/Person/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get(int id)
        {
            var person = await _personLogic.GetAsync(id);
            var info = new QueryResult<PersonDto>(person);
            return Ok(info);
        }

        // POST api/Person/GetByIds
        [HttpPost("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetByIds([FromBody] int[] ids)
        {
            var persons = await _personLogic.GetByIdsAsync(ids);
            var info = new QueryResult<IEnumerable<PersonDto>>(persons.ToList());
            return Ok(info);
        }

        // GET api/Person?searchText=5
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Search([FromQuery] string searchText)
        {
            var persons = await _personLogic.SearchAsync(searchText);
            var info = new QueryResult<IEnumerable<PersonDto>>(persons);
            return Ok(info);
        }
    }
}