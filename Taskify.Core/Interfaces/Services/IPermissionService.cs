using Taskify.Core.Enums;

namespace Taskify.Core.Interfaces.Services
{
    public interface IPermissionService
    {
        Task<HashSet<Permission>> GetPermissionsAsync(Guid userId, Guid projectId);
    }
}