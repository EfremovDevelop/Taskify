using Microsoft.AspNetCore.Mvc;
using Taskify.API.Contracts;
using Taskify.Core.Interfaces.Services;
using Taskify.Core.Models;

namespace Taskify.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsService _projectService;

        public ProjectsController(IProjectsService projectService)
        {
            _projectService = projectService;
        }

        // GET: api/<ProjectsController>
        [HttpGet]
        public async Task<ActionResult<ProjectsResponse>> GetProjects()
        {
            var projects = await _projectService.GetAllProjects();

            var response = projects
                .Select(p => new ProjectsResponse(p.Id, p.Name, p.Description, p.CreatedDate));

            return Ok(response);
        }

        // POST: api/<ProjectsController>
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateProject([FromBody] ProjectsRequest request)
        {
            var (project, err) = Project.Create(
                Guid.NewGuid(),
                request.Name,
                request.Description,
                DateTime.UtcNow);

            if (!string.IsNullOrEmpty(err))
                return BadRequest(err);

            await _projectService.CreateProject(project);
            return Ok(project.Id);
        }
    }
}
