using Taskify.Core.Interfaces;
using Taskify.Core.Models;

namespace Taskify.Application.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly IRepository<Project> _repository;

        public ProjectsService(IRepository<Project> repository)
        {
            _repository = repository;
        }

        public async Task<List<Project>> GetAllProjects()
        {
            return await _repository.GetList();
        }

        public async Task<Project> GetProject(Guid id)
        {
            return await _repository.GetItem(id);
        }

        public async Task<Guid> CreateProject(Project project)
        {
            return await _repository.Create(project);
        }

        public async Task<Guid> UpdateProject(Project project)
        {
            return await _repository.Update(project);
        }

        public async Task<Guid> DeleteProject(Guid id)
        {
            return await _repository.Delete(id);
        }
    }
}
