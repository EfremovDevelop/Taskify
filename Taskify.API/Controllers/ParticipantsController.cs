using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taskify.API.Contracts.Users;
using Taskify.Application.Services;
using Taskify.Core.Enums;
using Taskify.Core.Interfaces.Services;
using Taskify.Core.Models;

namespace Taskify.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantsController : ControllerBase
    {
        private readonly UsersService _usersService;
        private readonly IProjectUsersService _projectUsersService;
        private readonly IAuthorizationService _authorizationService;

        public ParticipantsController(UsersService usersService, IProjectUsersService projectUsersService, IAuthorizationService authorizationService)
        {
            _usersService = usersService;
            _projectUsersService = projectUsersService;
            _authorizationService = authorizationService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddParticipant([FromBody] ParticipantRequest request)
        {
            var email = request.Email;
            var projectId = request.ProjectId;

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, projectId, "AdminPolicy");
            // Если авторизация не прошла, возвращаем ошибку доступа
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email is required");
            }

            var user = await _usersService.GetUserByEmail(email);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var projectUserId = await _projectUsersService.AddParticipant(user.Id, projectId, Core.Enums.Role.User);

            return Ok(projectUserId);
        }
    }
}
