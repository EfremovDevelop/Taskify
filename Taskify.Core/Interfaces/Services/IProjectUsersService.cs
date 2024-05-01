using Taskify.Core.Models;

namespace Taskify.Core.Interfaces.Services;

public interface IProjectUsersService
{
    Task<Guid> AddParticipant(Guid userId, Guid projectId, Enums.Role role);
    Task<Guid> CreateUserProject(Guid userId, Project project);
    Task<ProjectUser> GetProjectUser(Guid userId, Guid projectId);
    Task<List<ProjectUser>> GetProjectUsers(Guid projectId);
    Task<List<Project>> GetUserProjects(Guid userId);
}