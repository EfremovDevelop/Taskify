using Taskify.Core.Enums;
using Taskify.Core.Interfaces.Repositories;
using Taskify.Core.Interfaces.Services;

namespace Taskify.Application.Services;

public class PermissionService : IPermissionService
{
    private readonly IProjectUsersRepository _repository;

    public PermissionService(IProjectUsersRepository repository)
    {
        _repository = repository;
    }

    public Task<HashSet<Permission>> GetPermissionsAsync(Guid userId, Guid projectId)
    {
        return _repository.GetProjectUserPermissions(userId, projectId);
    }
}
