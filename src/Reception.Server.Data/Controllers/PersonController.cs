using Microsoft.AspNetCore.Mvc;
using Reception.Model.Dto;
using Reception.Model.Network;
using Reception.Server.Auth.ConnectionLibrary;
using Reception.Server.Core.Interfaces;
using Reception.Server.Data.Logic;

namespace Reception.Server.Data.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public class PersonController : ControllerBase, IBaseController
    {
        private readonly IPersonLogic _personLogic;

        public PersonController(IPersonLogic personLogic)
        {
            _personLogic = personLogic;
        }

        // GET api/Person/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken = default)
        {
            var person = await _personLogic.GetAsync(id, cancellationToken);
            var info = new QueryResult<PersonDto>(person);
            return Ok(info);
        }

        // POST api/Person/GetByIds
        [HttpPost("[action]")]
        public async Task<IActionResult> GetByIds([FromBody] int[] ids, CancellationToken cancellationToken = default)
        {
            var persons = await _personLogic.GetByIdsAsync(ids, cancellationToken);
            var info = new QueryResult<IEnumerable<PersonDto>>(persons.ToList());
            return Ok(info);
        }

        // GET api/Person?searchText=5
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string searchText, CancellationToken cancellationToken = default)
        {
            var persons = await _personLogic.SearchAsync(searchText, cancellationToken);
            var info = new QueryResult<IEnumerable<PersonDto>>(persons);
            return Ok(info);
        }
    }
}