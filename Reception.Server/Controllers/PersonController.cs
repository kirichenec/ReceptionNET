using Microsoft.AspNetCore.Mvc;
using Reception.Model.Network;
using Reception.Server.Logic;
using Reception.Server.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reception.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonLogic _personLogic;

        public PersonController(IPersonLogic personLogic)
        {
            _personLogic = personLogic;
        }

        // GET api/Person/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonAsync(int id)
        {
            var person = await _personLogic.GetPersonAsync(id);
            var info = new QueryResult<Person> { Data = person, ErrorCode = person == null ? ErrorCode.Ok : ErrorCode.NotFound };
            return Ok(info);
        }

        // GET api/Person?searchText=5
        [HttpGet]
        public async Task<IActionResult> SearchPersonAsync([FromQuery]string searchText = "")
        {
            var persons = await _personLogic.SearchPersonAsync(searchText);
            var info = new QueryResult<List<Person>> { Data = persons, ErrorCode = persons.Any() ? ErrorCode.Ok : ErrorCode.NotFound };
            return Ok(info);
        }
    }
}