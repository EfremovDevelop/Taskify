using Taskify.Core.Interfaces.Repositories;
using Taskify.Core.Interfaces.Services;
using Taskify.Core.Models;

namespace Taskify.Application.Services;

public class ProjectsService : IProjectsService
{
    private readonly IProjectsRepository _repository;

    public ProjectsService(IProjectsRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Project>> GetAllProjects()
    {
        return await _repository.GetList();
    }

    public async Task<Project> GetProject(Guid id)
    {
        return await _repository.GetItem(id);
    }

    public async Task<Guid> CreateProject(Project project)
    {
        return await _repository.Create(project);
    }

    public async Task<Guid> UpdateProject(Project project)
    {
        return await _repository.Update(project);
    }

    public async Task<Guid> DeleteProject(Guid id)
    {
        return await _repository.Delete(id);
    }

    public async Task<List<Issue>> GetProjectIssues(Guid projectId)
    {
        return await _repository.GetProjectIssues(projectId);
    }
}