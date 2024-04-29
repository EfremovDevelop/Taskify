using Taskify.Core.Enums;
using Taskify.Core.Models;

namespace Taskify.Core.Interfaces.Repositories;

public interface IProjectUsersRepository
{
    Task<Guid> AddParticipant(Guid userId, Guid projectId, Enums.Role role);
    Task<Guid> CreateUserProject(Guid userId, Project project);
    Task<ProjectUser> GetProjectUser(Guid userId, Guid projectId);
    Task<HashSet<Permission>> GetProjectUserPermissions(Guid userId, Guid projectId);
    Task<List<User>> GetProjectUsers(Guid projectId);
    Task<List<Project>> GetUserProjects(Guid userId);
}