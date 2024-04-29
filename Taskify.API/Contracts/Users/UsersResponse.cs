using Taskify.Core.Models;

namespace Taskify.API.Contracts.Users;

public record UsersResponse(Guid Id, string UserName, string Email);

public record ProjectUserResponse(Guid userId, Guid projectId, string UserName, string Email, Role Role);