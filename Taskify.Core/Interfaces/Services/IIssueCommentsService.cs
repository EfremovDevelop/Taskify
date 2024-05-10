using Taskify.Core.Models;

namespace Taskify.Core.Interfaces.Services
{
    public interface IIssueCommentsService
    {
        Task<int> Create(IssueComment comment);
        Task<List<IssueComment>> GetComments(Guid issueId);
    }
}