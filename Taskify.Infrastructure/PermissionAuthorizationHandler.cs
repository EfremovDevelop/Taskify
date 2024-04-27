using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

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

        //var userId = context.User.Claims.FirstOrDefault(
        //    c => c.Type == CustomClaims.UserId);

        //if (userId is null || !Guid.TryParse(userId.Value, out var id))
        //{
        //    return;
        //}

        //using var scope = _serviceScopeFactory.CreateScope();

        //context.Succeed(requirement);

        //var projectId = projectService. 
        // Проверяем, есть ли projectId в контексте запроса
        if (context.Resource is HttpContext httpContext)
        {
            if (httpContext.Request.RouteValues.TryGetValue("id", out var projectIdObj) &&
                 projectIdObj is string projectIdString && Guid.TryParse(projectIdString, out var projectId))
            {
                // Успешно авторизован
                Guid idfd = projectId;
                var userId = context.User.Claims.FirstOrDefault(
                    c => c.Type == CustomClaims.UserId);

                if (userId is null || !Guid.TryParse(userId.Value, out var id))
                {
                    return;
                }

                using var scope = _serviceScopeFactory.CreateScope();
                context.Succeed(requirement);
            }
        }
    }
}
