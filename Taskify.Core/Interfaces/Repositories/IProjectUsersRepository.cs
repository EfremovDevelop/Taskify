using Taskify.Core.Enums;
using Taskify.Core.Models;

namespace Taskify.Core.Interfaces.Repositories;

public interface IProjectUsersRepository
{
    Task<Guid> CreateUserProject(Guid userId, Project project);
    Task<HashSet<Permission>> GetProjectUserPermissions(Guid userId, Guid projectId);
    Task<List<Project>> GetUserProjects(Guid userId);
}