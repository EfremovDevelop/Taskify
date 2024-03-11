using Taskify.Core.Interfaces;
using Taskify.Core.Models;

namespace Taskify.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IRepository<Project> _repository;

        public ProjectService(IRepository<Project> repository)
        {
            _repository = repository;
        }

        public async Task<List<Project>> GetAllProjects()
        {
            return await _repository.GetList();
        }

        public async Task<Project> GetProject(int id)
        {
            return await _repository.GetItem(id);
        }

        public async Task<int> CreateProject(Project project)
        {
            return await _repository.Create(project);
        }

        public async Task<int> UpdateProject(Project project)
        {
            return await _repository.Update(project);
        }

        public async Task<int> DeleteProject(int id)
        {
            return await _repository.Delete(id);
        }
    }
}
