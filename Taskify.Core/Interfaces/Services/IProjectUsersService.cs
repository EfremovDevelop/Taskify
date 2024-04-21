using Taskify.Core.Models;

namespace Taskify.Core.Interfaces.Services;

public interface IProjectUsersService
{
    Task<Guid> CreateUserProject(Guid userId, Project project);
    Task<List<Project>> GetUserProjects(Guid userId);
}