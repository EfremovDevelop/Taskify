using Taskify.Core.Enums;
using Taskify.Core.Interfaces.Repositories;

namespace Taskify.Core.Interfaces.Services;

public class PermissionService : IPermissionService
{
    private readonly IProjectUsersRepository _reposotory;

    public PermissionService(IProjectUsersRepository projectsRepository)
    {
        _reposotory = projectsRepository;
    }

    public Task<HashSet<Permission>> GetPermissionsAsync(Guid userId, Guid projectId)
    {
        return _reposotory.GetProjectUserPermissions(userId, projectId);
    }
}
