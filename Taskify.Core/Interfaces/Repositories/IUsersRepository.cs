using Taskify.Core.Models;

namespace Taskify.Core.Interfaces.Repositories;

public interface IUsersRepository
{
    Task<Guid> Create(User item);
    Task<User> GetByEmail(string email);
    Task<User?> GetById(Guid? id);
}