﻿using Microsoft.EntityFrameworkCore;
using System;
using Taskify.Core.Interfaces.Repositories;
using Taskify.Core.Models;
using Taskify.DataAccess.Entities;

namespace Taskify.DataAccess.Repositories
{
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
                Status = (StatusEntity)item.Status,
                ProjectId = item.ProjectId,
                RefId = item.RefId
            };
            await _context.Issue.AddAsync(issueEntity);
            await _context.SaveChangesAsync();
            return issueEntity.Id;
        }

        public async Task<Guid> Delete(Guid projectId, Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Issue> GetItem(Guid projectId, Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetMaxRefId(Guid projectId)
        {
            var issuesByProject = await _context.Issue
                .Where(i => i.ProjectId == projectId).ToListAsync();

            var maxRefId = issuesByProject.Max(i => (int?)i.RefId) ?? 0;
            return maxRefId;
        }

        public async Task<List<Issue>> GetList(Guid projectId)
        {
            var issueEntitiesByProject = await _context.Issue
                .Where(i => i.ProjectId == projectId).ToListAsync();

            var issuesByProject = issueEntitiesByProject
                .Select(i => Issue.Create(i.Id, i.Name, i.Description,
                i.TimeSpent, i.ProjectId, (Status)i.Status, i.CreatedDate, i.UpdatedDate, i.RefId).Issue)
                .ToList();

            return issuesByProject;

        }

        public async Task<Guid> Update(Issue item)
        {
            throw new NotImplementedException();
        }
    }
}

