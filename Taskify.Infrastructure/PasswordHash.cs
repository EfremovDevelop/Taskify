using Taskify.Core.Interfaces.Auth;

namespace Taskify.Infrastructure;

public class PasswordHash : IPasswordHash
{
    public string Generate(string password) =>
        BCrypt.Net.BCrypt.EnhancedHashPassword(password);

    public bool Verify(string password, string passwordHash) =>
        BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);
}
