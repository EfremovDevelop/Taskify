using Taskify.Core.Models;

namespace Taskify.Core.Interfaces.Auth;

public interface IJwtProvider
{
    string GenerateToken(User user);
}