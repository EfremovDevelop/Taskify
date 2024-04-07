using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taskify.API.Contracts.Users;
using Taskify.Application.Services;

namespace Taskify.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;

        public UsersController(UsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UsersRequest usersRequest)
        {
            await _usersService.Register(
                usersRequest.UserName,
                usersRequest.Email,
                usersRequest.Password);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
        {
            var token = await _usersService.Login(request.Email, request.Password);
            return Ok(token);
        }
    }
}
