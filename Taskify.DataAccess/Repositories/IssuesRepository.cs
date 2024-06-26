﻿using Microsoft.EntityFrameworkCore;
using Taskify.Core.Interfaces.Repositories;
using Taskify.Core.Models;
using Taskify.DataAccess.Entities;

namespace Taskify.DataAccess.Repositories;

public class IssuesRepository : IIssuesRepository
{
    private readonly DataContext _context;
    public IssuesRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Guid> Create(Issue item)
    {
        var issueEntity = new IssueEntity
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            CreatedDate = item.CreatedDate,
            UpdatedDate = item.UpdatedDate,
            TimeSpent = item.TimeSpent,
            StatusId = item.StatusId,
            ProjectId = item.ProjectId,
            RefId = item.RefId,
            AssignedUserId = item.AssignedUserId
        };
        await _context.Issue.AddAsync(issueEntity);
        await _context.SaveChangesAsync();
        return issueEntity.Id;
    }

    public async Task<Guid> Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Issue> GetItem(Guid id)
    {
        var issueEntity = await _context.Issue.FindAsync(id);

        return Issue.Create(id, issueEntity.Name, issueEntity.Description, issueEntity.TimeSpent,
            issueEntity.ProjectId, issueEntity.StatusId, issueEntity.CreatedDate, issueEntity.UpdatedDate, issueEntity.RefId, issueEntity.AssignedUserId).Issue;
    }

    public async Task<int> GetMaxRefId(Guid projectId)
    {
        var issuesByProject = await _context.Issue
            .Where(i => i.ProjectId == projectId).ToListAsync();

        var maxRefId = issuesByProject.Max(i => (int?)i.RefId) ?? 0;
        return maxRefId;
    }

    public async Task<List<Issue>> GetList()
    {
        var issueEntities = await _context.Issue.ToListAsync();

        var issues = issueEntities
            .Select(i => Issue.Create(i.Id, i.Name, i.Description,
            i.TimeSpent, i.ProjectId, i.StatusId, i.CreatedDate, i.UpdatedDate, i.RefId, i.AssignedUserId).Issue)
            .ToList();

        return issues;

    }

    public async Task<Guid> Update(Issue item)
    {
        await _context.Issue
            .Where(i => i.Id == item.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.Name, p => item.Name)
                .SetProperty(p => p.Description, p => item.Description)
                .SetProperty(p => p.StatusId, p => item.StatusId)
                .SetProperty(p => p.UpdatedDate, p => item.UpdatedDate)
                .SetProperty(p => p.TimeSpent, p => item.TimeSpent));
        await _context.SaveChangesAsync();
        return item.Id;
    }
}