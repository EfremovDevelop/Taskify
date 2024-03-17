using Taskify.Core.Models;

namespace Taskify.Core.Interfaces.Repositories
{
    public interface IIssuesRepository
    {
        Task<List<Issue>> GetList();

        Task<Issue> GetItem(Guid id);

        Task<Guid> Create(Issue item);
        Task<Guid> Update(Issue item);
        Task<Guid> Delete(Guid id);
        Task<int> GetMaxRefId(Guid projectId);
    }
}
