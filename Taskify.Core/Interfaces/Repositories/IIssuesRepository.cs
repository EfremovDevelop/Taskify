using Taskify.Core.Models;

namespace Taskify.Core.Interfaces.Repositories
{
    public interface IIssuesRepository
    {
        Task<Issue> GetItem(Guid projectId, Guid id);
        Task<List<Issue>> GetList(Guid projectId);

        Task<Guid> Create(Issue item);
        Task<Guid> Update(Issue item);
        Task<Guid> Delete(Guid projectId, Guid id);

        Task<int> GetMaxRefId(Guid projectId);
    }
}
