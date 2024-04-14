using Microsoft.AspNetCore.Authorization;
using Taskify.Core.Enums;

namespace Taskify.Infrastructure;

public class PermissionRequirement(Permission[] permissions) : IAuthorizationRequirement
{
    public Permission[] Permissions { get; set; } = permissions;
}
