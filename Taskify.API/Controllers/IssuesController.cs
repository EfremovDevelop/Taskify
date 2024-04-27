using Microsoft.AspNetCore.Mvc;
using Taskify.API.Contracts.Issues;
using Taskify.Core.Interfaces.Services;
using Taskify.Core.Models;

namespace Taskify.API.Controllers;

[Route("api/issues")]
[ApiController]
public class IssuesController : ControllerBase
{
    private readonly IIssuesService _issueService;

    public IssuesController(IIssuesService issueService)
    {
        _issueService = issueService;
    }

    // GET: api/<ProjectsController>
    [HttpGet]
    public async Task<ActionResult<IssuesResponse>> GetAllIssues()
    {
        var issues = await _issueService.GetAllIssues();

        var response = issues
            .Select(i => new IssuesResponse(
                i.Id, i.Name, i.Description, i.TimeSpent,
                i.StatusId, i.CreatedDate, i.UpdatedDate, i.RefId));

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateIssue([FromBody] IssuesRequest request)
    {
        var (issue, err) = Issue.Create(
            Guid.NewGuid(),
            request.Name,
            request.Description,
            request.TimeSpent,
            request.ProjectId,
            request.StatusId,
            DateTime.UtcNow,
            DateTime.UtcNow,
            0);

        if (!string.IsNullOrEmpty(err))
            return BadRequest(err);

        await _issueService.CreateIssue(issue);
        return Ok(issue.Id);
    }

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<IssuesResponse>> GetProject(Guid id)
    {
        var issue = await _issueService.GetIssue(id);

        var response = new IssuesResponse(
            id, issue.Name, issue.Description,
            issue.TimeSpent, issue.StatusId,
            issue.CreatedDate, issue.UpdatedDate, issue.RefId);

        return Ok(response);
    }
}

