using Taskify.Core.Enums;
using Taskify.Core.Models;

namespace Taskify.Core.Interfaces.Repositories;

public interface IProjectsRepository 
{
    Task<List<Project>> GetList();

    Task<Project> GetItem(Guid id);

    Task<Guid> Create(Project item);
    Task<Guid> Update(Project item);
    Task<Guid> Delete(Guid id);

    Task<List<Issue>> GetProjectIssues(Guid projectId);
}
