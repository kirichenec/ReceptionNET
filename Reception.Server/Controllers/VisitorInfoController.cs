using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Reception.Model.Dto;
using Reception.Model.Network;

namespace Reception.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorInfoController : ControllerBase
    {
        IConfiguration _configuration;

        public VisitorInfoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET api/VisitorInfo/5
        [HttpGet("{id}")]
        public ActionResult<string> GetVisitorInfo(int id)
        {
            var info =
                new QueryResult<PersonDto>
                {
                    Data =
                        new PersonDto
                        {
                            FirstName = "Igor",
                            MiddleName = "G",
                            PhotoPath = Path.Combine(_configuration.GetSection("AppSettings").GetSection("ImagesFolder").Value, "VisitorDefault.jpg"),
                            SecondName = "K",
                            Uid = 1
                        }
                };
            return Ok(info);
        }

        // POST api/VisitorInfo
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}
