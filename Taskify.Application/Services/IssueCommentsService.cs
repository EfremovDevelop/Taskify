using Taskify.Core.Interfaces.Repositories;
using Taskify.Core.Interfaces.Services;
using Taskify.Core.Models;

namespace Taskify.Application.Services;

public class IssueCommentsService : IIssueCommentsService
{
    private readonly IIssueCommentsRepository _issueCommentsRepository;

    public IssueCommentsService(IIssueCommentsRepository issueCommentsRepository)
    {
        _issueCommentsRepository = issueCommentsRepository;
    }

    public async Task<int> Create(IssueComment comment)
    {
        return await _issueCommentsRepository.Create(comment);
    }

    public async Task<List<IssueComment>> GetComments(Guid issueId)
    {
        return await _issueCommentsRepository.GetComments(issueId);
    }
}

