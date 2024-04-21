using Microsoft.AspNetCore.Mvc;
using Taskify.API.Contracts.Projects;
using Taskify.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Taskify.Infrastructure;
using Taskify.Core.Models;

namespace Taskify.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UserProjectsController : ControllerBase
{
    private readonly IProjectUsersService _projectUsersService;

    public UserProjectsController(IProjectUsersService projectUsersService)
    {
        _projectUsersService = projectUsersService;
    }

    [HttpGet]
    public async Task<ActionResult<ProjectsResponse>> GetUserProjects()
    {
        Guid userId = GetUserId();
        if (userId == Guid.Empty)
            return NotFound();

        var response = await _projectUsersService.GetUserProjects(userId);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateUserProject([FromBody] ProjectsRequest request)
    {
        Guid userId = GetUserId();
        if (userId == Guid.Empty)
            return NotFound();

        var (project, err) = Project.Create(
            Guid.NewGuid(),
            request.Name,
            request.Description,
            DateTime.Now);

        if (!string.IsNullOrEmpty(err))
            return BadRequest(err);

        await _projectUsersService.CreateUserProject(userId, project);
        return Ok(project.Id);
    }

    private Guid GetUserId()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == CustomClaims.UserId)?.Value;

        if (userId == null)
            return Guid.Empty;

        return Guid.Parse(userId);
    }
}
