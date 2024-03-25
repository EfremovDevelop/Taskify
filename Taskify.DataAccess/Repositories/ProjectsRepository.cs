using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Taskify.Core.Interfaces.Repositories;
using Taskify.Core.Models;
using Taskify.DataAccess;
using Taskify.DataAccess.Entities;

namespace Taskify.Core.Interfaces
{
    public class ProjectsRepository : IProjectsRepository
    {
        //protected readonly ILogger _logger;
        private readonly DataContext _context;
        public ProjectsRepository(DataContext context)
        { 
            _context = context;
        }

        public async Task<Guid> Create(Project item)
        {
            var projectEntity = new ProjectEntity
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                CreatedDate = DateTime.UtcNow
            };
            await _context.Project.AddAsync(projectEntity);
            await _context.SaveChangesAsync();
            return projectEntity.Id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Project
                .Where(p => p.Id == id)
                .ExecuteDeleteAsync();
            return id;
        }

        public async Task<Project> GetItem(Guid id)
        {
            var projectEntity = await _context.Project.FindAsync(id);
            if (projectEntity == null)
            {
                return null;
            }
            var project = Project
                .Create(projectEntity.Id, 
                    projectEntity.Name, 
                    projectEntity.Description, 
                    projectEntity.CreatedDate).Project;

            return project;
        }

        public async Task<List<Project>> GetList()
        {
            var projectEntities = await _context.Project.ToListAsync();

            var projects = projectEntities
                .Select(p => Project.Create(p.Id, p.Name, p.Description, p.CreatedDate).Project)
                .ToList();

            return projects;
        }

        public async Task<List<Issue>> GetProjectIssues(Guid projectId)
        {
            var issueEntitiesByProject = await _context.Issue
                .Where(i => i.ProjectId == projectId).ToListAsync();

            var issuesByProject = issueEntitiesByProject
                .Select(i => Issue.Create(i.Id, i.Name, i.Description,
                i.TimeSpent, i.ProjectId, (Status)i.Status, i.CreatedDate, i.UpdatedDate, i.RefId).Issue)
                .ToList();

            return issuesByProject;
        }

        public async Task<Guid> Update(Project item)
        {
            await _context.Project
                .Where(p => p.Id == item.Id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(p => p.Name, p => item.Name)
                    .SetProperty(p => p.Description, p => item.Description));

            return item.Id;
        }
    }
}
