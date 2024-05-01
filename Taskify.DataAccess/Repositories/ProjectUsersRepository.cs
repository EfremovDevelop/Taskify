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

        return await AddParticipant(userId, projectEntity.Id, Core.Enums.Role.Admin);
    }

    public async Task<Guid> AddParticipant(Guid userId, Guid projectId, Core.Enums.Role role)
    {
        var roleEntity = await _context.Role
            .SingleOrDefaultAsync(r => r.Id == (int)role)
            ?? throw new InvalidOperationException();
        
        var projectUserEntity = new ProjectUserEntity
        {
            UserId = userId,
            ProjectId = projectId,
            Roles = [roleEntity]
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

    public async Task<List<ProjectUser>> GetProjectUsers(Guid projectId)
    {
        var projectUsers = await _context.ProjectUser
            .Include(pu => pu.Roles)
            .AsNoTracking()
            .Where(pu => pu.ProjectId == projectId)
            .ToListAsync();

        var usersWithRoles = new List<ProjectUser>();

        foreach (var pu in projectUsers)
        {
            var userEntity = await _context.User
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == pu.UserId);

            if (userEntity != null)
            {
                var roleEntity = pu.Roles.FirstOrDefault(); // предполагается, что у пользователя может быть только одна роль
                var role = Core.Models.Role.Create(roleEntity.Id, roleEntity.Name).Role;

                var projectUser = ProjectUser.Create(userEntity.Id,
                    userEntity.Name, userEntity.Email, role).ProjectUser;

                usersWithRoles.Add(projectUser);
            }
        }

        return usersWithRoles;
    }

    public async Task<ProjectUser> GetProjectUser(Guid userId, Guid projectId)
    {
        var userEntity = await _context.User
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == userId);
        if (userEntity == null)
        {
            throw new Exception("No User data");
        }
        var projectUserEntity = await _context.ProjectUser
            .Include(pu => pu.Roles)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.ProjectId == projectId && u.UserId == userId);

        if (projectUserEntity == null)
        {
            throw new Exception("No ProjectUser data");
        }

        var roleEntity = projectUserEntity.Roles.FirstOrDefault(); // пока что будет одна роль

        var role = Core.Models.Role.Create(roleEntity.Id, roleEntity.Name).Role;

        var projectUser = ProjectUser.Create(userId, userEntity.Name, userEntity.Email, role).ProjectUser;

        return projectUser;
    }
}
