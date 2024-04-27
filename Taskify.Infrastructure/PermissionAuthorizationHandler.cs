using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Taskify.Core.Interfaces.Services;

namespace Taskify.Infrastructure;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var userId = context.User.Claims.FirstOrDefault(
                    c => c.Type == CustomClaims.UserId);

        if (userId is null || !Guid.TryParse(userId.Value, out var id))
        {
            return;
        }

        if (context.Resource is HttpContext httpContext)
        {
            if (httpContext.Request.RouteValues.TryGetValue("id", out var projectIdObj) &&
                 projectIdObj is string projectIdString && Guid.TryParse(projectIdString, out var projectId))
            {
                bool checkPolicy = await CheckPolicyAsync(id, projectId, requirement);
                if (checkPolicy)
                {
                    context.Succeed(requirement);
                    return;
                }
            }
        }
        else if (context.Resource is not null)
        {
            if (context.Resource is Guid projectId)
            {
                bool checkPolicy = await CheckPolicyAsync(id, projectId, requirement);
                if (checkPolicy)
                {
                    context.Succeed(requirement); // Политика доступа выполнена
                    return;
                }
            }
        }
    }

    private async Task<bool> CheckPolicyAsync(Guid userId, Guid projectId, PermissionRequirement requirement)
    {
        using var scope = _serviceScopeFactory.CreateScope();

        var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();

        var permissions = await permissionService.GetPermissionsAsync(userId, projectId);

        if (permissions.Intersect(requirement.Permissions).Any())
        {
            return true;
        }
        return false;
    }
}
