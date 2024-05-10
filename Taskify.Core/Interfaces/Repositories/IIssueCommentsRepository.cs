using Taskify.Core.Models;

namespace Taskify.Core.Interfaces.Repositories
{
    public interface IIssueCommentsRepository
    {
        Task<int> Create(IssueComment comment);
        Task<List<IssueComment>> GetComments(Guid issueId);
    }
}