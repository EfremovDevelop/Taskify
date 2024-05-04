using Taskify.Core.Interfaces.Auth;
using Taskify.Core.Interfaces.Repositories;
using Taskify.Core.Models;

namespace Taskify.Application.Services;

public class UsersService
{
    private readonly IPasswordHash _passwordHash;
    private readonly IUsersRepository _usersRepository;
    private readonly IJwtProvider _jwtProvider;

    public UsersService(
        IJwtProvider jwtProvider,
        IUsersRepository usersRepository, 
        IPasswordHash passwordHash) 
    {
        _passwordHash = passwordHash;
        _usersRepository = usersRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task Register(string userName, string email, string password)
    {
        var existingUser = await _usersRepository.GetByEmail(email);
        if (existingUser != null)
        {
            // Пользователь с таким email уже существует
            return;
        }
        var hashPassword = _passwordHash.Generate(password);

        var (user, err) = User.Create(Guid.NewGuid(), userName, email, hashPassword);

        if (!string.IsNullOrEmpty(err))
            return;

        await _usersRepository.Create(user);
    }

    public async Task<string> Login(string email, string password)
    {
        var user = await _usersRepository.GetByEmail(email);

        var result = _passwordHash.Verify(password, user.Password);

        if (result == false)
            throw new Exception("Failed to login");

        var token = _jwtProvider.GenerateToken(user);

        return token;
    }

    public async Task<User> GetUserByEmail(string email)
    {
        return await _usersRepository.GetByEmail(email);
    }

    public async Task<User?> GetUserById(Guid? id)
    {
        if (id == null)
            return null;
        return await _usersRepository.GetById(id);
    }
}
