using System;
using Taskify.Core.Interfaces;
using Taskify.Core.Models;
using Taskify.DataAccess.Entities;

namespace Taskify.DataAccess.Repositories
{
	public class IssuesRepository : IRepository<Issue>
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
                PathId = item.PathId
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
            throw new NotImplementedException();
        }

        public async Task<List<Issue>> GetList()
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> Update(Issue item)
        {
            throw new NotImplementedException();
        }
    }
}

