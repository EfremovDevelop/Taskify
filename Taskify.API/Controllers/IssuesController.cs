using Microsoft.AspNetCore.Mvc;
using Taskify.API.Contracts.Issues;
using Taskify.Core.Interfaces.Services;

namespace Taskify.API.Controllers
{
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
    }
}

