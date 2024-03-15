using Taskify.Core.Models;

namespace Taskify.Core.Interfaces
{
    public interface IProjectsService
    {
        Task<Project> GetProject(Guid id);
        Task<List<Project>> GetAllProjects();
        Task<Guid> CreateProject(Project project);
        Task<Guid> DeleteProject(Guid id);
        Task<Guid> UpdateProject(Project project);
    }
}