using Taskify.Core.Enums;
using Taskify.Core.Interfaces.Repositories;

namespace Taskify.Core.Interfaces.Services;

public class PermissionService : IPermissionService
{
    private readonly IProjectsRepository _projectsRepository;

    public PermissionService(IProjectsRepository projectsRepository)
    {
        _projectsRepository = projectsRepository;
    }

    public Task<HashSet<Permission>> GetPermissionsAsync(Guid userId, Guid projectId)
    {
        return _projectsRepository.GetProjectUserPermissions(userId, projectId);
    }
}
