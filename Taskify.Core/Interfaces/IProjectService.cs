using Taskify.Core.Models;

namespace Taskify.Core.Interfaces
{
    public interface IProjectService
    {
        Task<Project> GetProject(int id);
        Task<List<Project>> GetAllProjects();
        Task<int> CreateProject(Project project);
        Task<int> DeleteProject(int id);
        Task<int> UpdateProject(Project project);
    }
}