using Microsoft.EntityFrameworkCore;
using Taskify.Core.Enums;
using Taskify.Core.Interfaces.Repositories;
using Taskify.Core.Models;
using Taskify.DataAccess.Entities;

namespace Taskify.DataAccess.Repositories;

public class ProjectUsersRepository : IProjectUsersRepository
{
    private readonly DataContext _context;

    public ProjectUsersRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Guid> CreateUserProject(Guid userId, Project project)
    {
        var projectEntity = new ProjectEntity
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            CreatedDate = DateTime.UtcNow
        };
        await _context.Project.AddAsync(projectEntity);
        await _context.SaveChangesAsync();

        var projectUserEntity = new ProjectUserEntity
        {
            UserId = userId,
            ProjectId = projectEntity.Id
        };

        await _context.ProjectUser.AddAsync(projectUserEntity);
        await _context.SaveChangesAsync();

        return projectUserEntity.Id;
    }

    public async Task<List<Project>> GetUserProjects(Guid userId)
    {
        var projectUserEntities = await _context.ProjectUser
                                                .Where(pu => pu.UserId == userId)
                                                .ToListAsync();

        var projectIds = projectUserEntities.Select(pu => pu.ProjectId).ToList();

        var projectEntities = await _context.Project
                                            .Where(p => projectIds.Contains(p.Id))
                                            .ToListAsync();

        var projects = projectEntities
                            .Select(p => Project.Create(
                                p.Id,
                                p.Name,
                                p.Description,
                                p.CreatedDate)
                                .Project)
                            .ToList();

        return projects;
    }

    public async Task<HashSet<Permission>> GetProjectUserPermissions(Guid userId, Guid projectId)
    {
        var roles = await _context.ProjectUser
            .AsNoTracking()
            .Include(pu => pu.Roles)
            .ThenInclude(r => r.Permissions)
            .Where(pu => pu.ProjectId == projectId)
            .Select(pu => pu.Roles)
            .ToListAsync();

        return roles
            .SelectMany(r => r)
            .SelectMany(r => r.Permissions)
            .Select(p => (Permission)p.Id)
            .ToHashSet();
    }
}
