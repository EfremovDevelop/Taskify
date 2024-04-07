using Microsoft.EntityFrameworkCore;
using Taskify.Core.Interfaces.Repositories;
using Taskify.Core.Models;
using Taskify.DataAccess.Entities;

namespace Taskify.DataAccess.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DataContext _context;
        public UsersRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Guid> Create(User item)
        {
            var userEntity = new UserEntity
            {
                Id = item.Id,
                Name = item.Name,
                Email = item.Email,
                Password = item.Password
            };
            await _context.User.AddAsync(userEntity);
            await _context.SaveChangesAsync();
            return userEntity.Id;
        }

        public async Task<User> GetByEmail(string email)
        {
            var userEntity = await _context.User
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception();

            var (user, err) = User.Create(
                userEntity.Id,
                userEntity.Name,
                userEntity.Email,
                userEntity.Password);

            return user;
        }
    }
}
