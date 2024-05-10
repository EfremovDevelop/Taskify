using Microsoft.EntityFrameworkCore;
using Taskify.Core.Interfaces.Repositories;
using Taskify.Core.Models;
using Taskify.DataAccess.Entities;

namespace Taskify.DataAccess.Repositories;

public class IssueCommentsRepository : IIssueCommentsRepository
{
    private readonly DataContext _context;

    public IssueCommentsRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<int> Create(IssueComment comment)
    {
        var commentEntity = new IssueCommentEntity
        {
            Comment = comment.Comment,
            CreatedDate = comment.CreatedDate,
            UserId = comment.UserId,
            IssueId = comment.IssueId
        };

        await _context.IssueComment.AddAsync(commentEntity);
        await _context.SaveChangesAsync();

        return commentEntity.Id;
    }

    public async Task<List<IssueComment>> GetComments(Guid issueId)
    {
        var commentEntities = await _context.IssueComment
            .AsNoTracking()
            .Where(c => c.IssueId == issueId)
            .ToListAsync();
        var comments = commentEntities
            .Select(c => IssueComment.Create(c.Id, c.Comment,
            c.CreatedDate, c.UserId, c.IssueId).IssueComment).ToList();
        return comments;
    }
}

