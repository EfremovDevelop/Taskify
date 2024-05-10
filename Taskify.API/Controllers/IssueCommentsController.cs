using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taskify.API.Contracts.Comments;
using Taskify.API.Contracts.Users;
using Taskify.Application.Services;
using Taskify.Core.Interfaces.Services;
using Taskify.Core.Models;
using Taskify.Infrastructure;

namespace Taskify.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class IssueCommentsController : ControllerBase
{
    private readonly IIssueCommentsService _issueCommentsService;
    private readonly UsersService _usersService;

    public IssueCommentsController(IIssueCommentsService issueCommentsService, UsersService usersService)
    {
        _issueCommentsService = issueCommentsService;
        _usersService = usersService;
    }

    [HttpGet("{issueId:Guid}")]
    public async Task<ActionResult<IssueCommentsResponse>> GetIssueComments(Guid issueId)
    {
        var issueComments = await _issueCommentsService.GetComments(issueId);

        Guid? userId = GetUserId();

        if (userId == null)
            return NotFound();

        var user = await _usersService.GetUserById(userId);

        var userResponse = new UsersResponse(userId, user.Name, user.Email);

        var response = issueComments
            .Select(c => new IssueCommentsResponse(c.Id, c.Comment, c.CreatedDate, userResponse));
        return Ok(response);
    }

    [HttpPost("{issueId:Guid}")]
    public async Task<IActionResult> CreateIssueComment([FromBody] IssueCommentsRequest request, Guid issueId)
    {
        var userId = GetUserId();
        var issueComment = IssueComment.Create(request.Comment, DateTime.UtcNow, userId, issueId).IssueComment;
        await _issueCommentsService.Create(issueComment);

        return Ok(issueComment);
    }

    private Guid GetUserId()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == CustomClaims.UserId)?.Value;

        if (userId == null)
            return Guid.Empty;

        return Guid.Parse(userId);
    }
}
