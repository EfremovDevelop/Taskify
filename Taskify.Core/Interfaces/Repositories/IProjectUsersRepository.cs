using Taskify.Core.Models;

namespace Taskify.Core.Interfaces.Repositories;

public interface IProjectUsersRepository
{
    Task<Guid> CreateUserProject(Guid userId, Project project);
    Task<List<Project>> GetUserProjects(Guid userId);
}