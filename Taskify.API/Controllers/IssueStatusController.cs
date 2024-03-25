using Microsoft.AspNetCore.Mvc;
using Taskify.Core.Interfaces.Services;
using Taskify.DataAccess.Entities;

namespace Taskify.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IssueStatusController : Controller
    {
        private readonly IIssueStatusesService _statusesService;

        public IssueStatusController(IIssueStatusesService statusesService)
        {
            _statusesService = statusesService;
        }

        [HttpGet]
        public IActionResult GetIssueStatuses()
        {
            var statuses = _statusesService.GetIssueStatusesList();

            return Ok(statuses);
        }
    }
}
