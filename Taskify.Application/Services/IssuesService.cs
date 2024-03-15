﻿using Taskify.Core.Interfaces;
using Taskify.Core.Models;

namespace Taskify.Application.Services
{
	public class IssuesService : IIssueService
	{
        private readonly IRepository<Issue> _issuesRepository;
		public IssuesService(IRepository<Issue> issueRepository)
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

        public async Task<List<Issue>> GetAllIssues()
        {
            return await _issuesRepository.GetList();
        }

        public async Task<Issue> GetIssue(Guid id)
        {
            return await _issuesRepository.GetItem(id);
        }

        public async Task<Guid> UpdateIssue(Issue issue)
        {
            return await _issuesRepository.Update(issue);
        }
    }
}

