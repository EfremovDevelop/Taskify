using Microsoft.AspNetCore.Mvc;
using Taskify.API.Contracts;
using Taskify.Core.Interfaces;

namespace Taskify.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuesController : ControllerBase
	{
        private readonly IIssueService _issueService;

        public IssuesController(IIssueService issueService)
        {
            _issueService = issueService;
        }

        [HttpGet]
        public async Task<ActionResult<IssuesResponse>> GetIssues()
        {
            var issues = await _issueService.GetAllIssues();

            var response = issues.
                Select(i => new IssuesResponse(i.Name, i.Description, i.TimeSpent,
                i.Status, i.CreatedDate, i.UpdatedDate, i.PathId));

            return Ok(response);
        }
    }
}

