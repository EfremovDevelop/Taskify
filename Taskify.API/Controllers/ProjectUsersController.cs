using Microsoft.AspNetCore.Mvc;
using Taskify.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Taskify.API.Contracts.Users;
using Taskify.Infrastructure;

namespace Taskify.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ProjectUsersController : ControllerBase
{
    private readonly IProjectUsersService _projectUsersService;

    public ProjectUsersController(IProjectUsersService projectUsersService)
    {
        _projectUsersService = projectUsersService;
    }

    [HttpGet("{projectId:Guid}")]
    public async Task<ActionResult<UsersResponse>> GetProjectUsers(Guid projectId)
    {
        var users = await _projectUsersService.GetProjectUsers(projectId);

        var response = users
            .Select(u => new UsersResponse(u.Id, u.Name, u.Email));

        return Ok(response);
    }

    [HttpGet("{projectId:Guid}/me")]
    public async Task<ActionResult<UsersResponse>> GetProjectUser(Guid projectId)
    {
        var userId = GetUserId();
        var user = await _projectUsersService.GetProjectUser(userId, projectId);

        var response = new ProjectUserResponse(userId, projectId, user.Name, user.Email, user.Role);

        return Ok(response);
    }

    private Guid GetUserId()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == CustomClaims.UserId)?.Value;

        if (userId == null)
            return Guid.Empty;

        return Guid.Parse(userId);
    }
}
