using Taskify.Core.Interfaces.Repositories;
using Taskify.Core.Interfaces.Services;
using Taskify.Core.Models;

namespace Taskify.Application.Services;

public class ProjectUsersService : IProjectUsersService
{
    private readonly IProjectUsersRepository _projectUserRepository;

    public ProjectUsersService(IProjectUsersRepository projectUserRepository)
    {
        _projectUserRepository = projectUserRepository;
    }

    public async Task<Guid> CreateUserProject(Guid userId, Project project)
    {
        return await _projectUserRepository.CreateUserProject(userId, project);
    }

    public async Task<List<Project>> GetUserProjects(Guid userId)
    {
        return await _projectUserRepository.GetUserProjects(userId);
    }

    public async Task<List<ProjectUser>> GetProjectUsers(Guid projectId)
    {
        return await _projectUserRepository.GetProjectUsers(projectId);
    }

    public async Task<ProjectUser> GetProjectUser(Guid userId, Guid projectId)
    {
        return await _projectUserRepository.GetProjectUser(userId, projectId);
    }

    public async Task<Guid> AddParticipant(Guid userId, Guid projectId, Core.Enums.Role role)
    {
        return await _projectUserRepository.AddParticipant(userId, projectId, role);
    }
}
