using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taskify.API.Contracts;
using Taskify.API.Scripts;
using Taskify.Core.Interfaces.Services;

namespace Taskify.API.Controllers
{
    [Route("api/projects/{projectId:guid}/issues")]
    [ApiController]
    public class IssuesController : ControllerBase
	{
        private readonly IIssuesService _issueService;

        public IssuesController(IIssuesService issueService)
        {
            _issueService = issueService;
        }

        [HttpGet]
        public async Task<ActionResult<IssuesResponse>> GetProjectIssues(Guid projectId)
        {
            var issues = await _issueService.GetIssues(projectId);

            var response = issues.
                Select(i => new IssuesResponse(i.Name, i.Description, i.TimeSpent,
                EnumExtensions.GetDescription(i.Status), i.CreatedDate, i.UpdatedDate, i.RefId));

            return Ok(response);
        }
    }
}

