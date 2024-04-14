using System;
using Taskify.Core.Models;

namespace Taskify.Core.Interfaces.Services;

public interface IIssuesService
{
    Task<Issue> GetIssue(Guid id);
    Task<List<Issue>> GetAllIssues();
    Task<Guid> CreateIssue(Issue issue);
    Task<Guid> DeleteIssue(Guid id);
    Task<Guid> UpdateIssue(Issue issue);

    Task<int> GetMaxRefId(Guid projectId);
}

