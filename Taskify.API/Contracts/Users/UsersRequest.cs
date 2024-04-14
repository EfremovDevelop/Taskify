using System.ComponentModel.DataAnnotations;

namespace Taskify.API.Contracts.Users;

public record UsersRequest (string UserName, string Email, string Password);
