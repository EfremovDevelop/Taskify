using System;
using Taskify.Core.Models;

namespace Taskify.Core.Interfaces.Services
{
    public interface IIssuesService
    {
        Task<Issue> GetIssue(Guid projectId, Guid id);
        Task<List<Issue>> GetIssues(Guid projectId);
        Task<Guid> CreateIssue(Issue issue);
        Task<Guid> DeleteIssue(Guid projectId, Guid id);
        Task<Guid> UpdateIssue(Issue issue);

        Task<int> GetMaxRefId(Guid projectId);
    }
}

