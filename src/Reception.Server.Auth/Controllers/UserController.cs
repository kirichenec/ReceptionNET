using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reception.Extension;
using Reception.Model.Network;
using Reception.Server.Auth.Helpers;
using Reception.Server.Auth.Logic;
using System.Threading;
using System.Threading.Tasks;

namespace Reception.Server.Auth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class UserController : ControllerBase
    {
        private readonly IUserLogic _userLogic;

        public UserController(IUserLogic userService)
        {
            _userLogic = userService;
        }

        // POST User/Authenticate
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequest model, CancellationToken token = default)
        {
            var response = await _userLogic.AuthenticateAsync(model, token);

            return response.HasNoValue()
                ? Unauthorized(new { message = "Username or password is incorrect" })
                : Ok(response);
        }

        // GET User/IsAuthValid
        [InternalServerAuthorize]
        [HttpGet("IsAuthValid")]
        public IActionResult IsAuthValid()
        {
            return Ok();
        }

        // PUT User
        [InternalServerAuthorize]
        [HttpPut]
        public async Task<IActionResult> CreateUser([FromBody] AuthenticateRequest model, CancellationToken token = default)
        {
            var savedUser = await _userLogic.CreateUserAsync(model.Login, model.Password, token);
            return Ok(savedUser.ToQueryResult());
        }

        // GET User?searchText=5
        [InternalServerAuthorize]
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string searchText, CancellationToken token = default)
        {
            var searchResult = await _userLogic.SearchAsync(searchText, token);
            return Ok(searchResult.ToQueryResult());
        }
    }
}