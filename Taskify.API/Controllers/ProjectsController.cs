using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taskify.API.Contracts;
using Taskify.Core.Interfaces.Services;
using Taskify.Core.Models;

namespace Taskify.API.Controllers
{
    [Authorize]
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
                DateTime.Now);

            if (!string.IsNullOrEmpty(err))
                return BadRequest(err);

            await _projectService.CreateProject(project);
            return Ok(project.Id);
        }

        [HttpGet("{projectId:Guid}/issues")]
        public async Task<ActionResult<IssuesResponse>> GetProjectIssues(Guid projectId)
        {
            var issues = await _projectService.GetProjectIssues(projectId);

            var response = issues.
                Select(i => new IssuesResponse(i.Id, i.Name, i.Description, i.TimeSpent,
                i.Status.ToString(), i.CreatedDate, i.UpdatedDate, i.RefId));

            return Ok(response);
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<Guid>> UpdateProject(Guid id, [FromBody] ProjectsRequest request)
        {
            var projectEntity = await _projectService.GetProject(id);
            if (projectEntity == null)
            {
                return BadRequest();
            }
            var (project, err) = Project.Create(
                id, request.Name, request.Description, projectEntity.CreatedDate);
            if (!string.IsNullOrEmpty(err))
            {
                return BadRequest(err);
            }
            var projectId = await _projectService.UpdateProject(project);
            return Ok(projectId);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<Guid>> DeleteProject(Guid id)
        {
            var projectId = await _projectService.DeleteProject(id);
            return Ok(projectId);
        }
    }
}
