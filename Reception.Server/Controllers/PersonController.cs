using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Reception.Model.Network;
using Reception.Server.Logic;
using Reception.Server.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reception.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private IConfiguration _configuration;
        private IPersonLogic _personLogic;

        public PersonController(IConfiguration configuration, IPersonLogic personLogic)
        {
            _configuration = configuration;
            _personLogic = personLogic;
        }

        // GET api/Person/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonAsync(int id)
        {
            var person = await _personLogic.GetPersonAsync(id);
            var info = new QueryResult<Person> { Data = person };
            return await Task.FromResult<IActionResult>(Ok(info));
        }

        // GET api/Person?searchText=5
        [HttpGet]
        public async Task<IActionResult> SearchPersonAsync([FromQuery]string searchText = "")
        {
            var person = await _personLogic.SearchPersonAsync(searchText);
            var info = new QueryResult<List<Person>> { Data = person };
            return await Task.FromResult<IActionResult>(Ok(info));
        }

        // POST api/VisitorInfo
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}
