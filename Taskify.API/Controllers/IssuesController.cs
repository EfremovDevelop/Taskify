using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taskify.API.Contracts.Issues;
using Taskify.Application.Services;
using Taskify.Core.Interfaces.Services;
using Taskify.Core.Models;

namespace Taskify.API.Controllers;

[Authorize]
[Route("api/issues")]
[ApiController]
public class IssuesController : ControllerBase
{
    private readonly IIssuesService _issueService;
    private readonly IAuthorizationService _authorizationService;
    private readonly UsersService _usersService;

    public IssuesController(IIssuesService issueService, IAuthorizationService authorizationService, UsersService usersService)
    {
        _issueService = issueService;
        _authorizationService = authorizationService;
        _usersService = usersService;
    }

    // GET: api/<ProjectsController>

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateIssue([FromBody] IssuesRequest request)
    {
        var authorizationResult = await _authorizationService.AuthorizeAsync(User, request.ProjectId, "AdminPolicy");
        if (!authorizationResult.Succeeded)
        {
            return Forbid();
        }
        var (issue, err) = Issue.Create(
            Guid.NewGuid(),
            request.Name,
            request.Description,
            request.TimeSpent,
            request.ProjectId,
            request.StatusId,
            DateTime.UtcNow,
            DateTime.UtcNow,
            0,
            request.AssignedId);

        if (!string.IsNullOrEmpty(err))
            return BadRequest(err);

        await _issueService.CreateIssue(issue);
        return Ok(issue.Id);
    }

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<IssuesResponse>> GetIssue(Guid id)
    {
        var issue = await _issueService.GetIssue(id);

        var authorizationResult = await _authorizationService.AuthorizeAsync(User, issue.ProjectId, "AdminPolicy");
        // Если авторизация не прошла, возвращаем ошибку доступа
        if (!authorizationResult.Succeeded)
        {
            return Forbid();
        }
        string? name = null;

        if (issue.AssignedUserId != null)
        {
            var user = await _usersService.GetUserById(issue.AssignedUserId);
            if (user == null)
            {
                return NotFound();
            }
            name = user.Name;
        }
        var response = new IssuesWirhAssignedResponse(
            id, issue.Name, issue.Description,
            issue.TimeSpent, issue.StatusId,
            issue.CreatedDate, issue.UpdatedDate, issue.RefId, name, issue.ProjectId,
            issue.AssignedUserId);

        return Ok(response);
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> UpdateIssue(Guid id, [FromBody] IssuesRequest issueUpdate)
    {
        var issueEntity = await _issueService.GetIssue(id);
        var issue = Issue.Create(issueEntity.Id,
            issueUpdate.Name, issueUpdate.Description,
            issueUpdate.TimeSpent, issueEntity.ProjectId, issueUpdate.StatusId,
            issueEntity.CreatedDate, DateTime.UtcNow, issueEntity.RefId, issueUpdate.AssignedId).Issue;

        await _issueService.UpdateIssue(issue);

        return Ok(issue);
    }
}

