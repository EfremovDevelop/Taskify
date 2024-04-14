using Taskify.Core.Interfaces.Repositories;
using Taskify.Core.Interfaces.Services;
using Taskify.Core.Models;

namespace Taskify.Application.Services;

public class IssuesService : IIssuesService
	{
    private readonly IIssuesRepository _issuesRepository;
		public IssuesService(IIssuesRepository issueRepository)
		{
        _issuesRepository = issueRepository;
		}

    public async Task<Guid> CreateIssue(Issue issue)
    {
        return await _issuesRepository.Create(issue);
    }

    public async Task<Guid> DeleteIssue(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Issue> GetIssue(Guid id)
    {
        return await _issuesRepository.GetItem(id);
    }

    public async Task<Guid> UpdateIssue(Issue issue)
    {
        return await _issuesRepository.Update(issue);
    }

    public async Task<int> GetMaxRefId(Guid projectId)
    {
        return await _issuesRepository.GetMaxRefId(projectId);
    }

    public async Task<List<Issue>> GetAllIssues()
    {
        return await _issuesRepository.GetList();
    }
}

