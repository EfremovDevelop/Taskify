using System.ComponentModel.DataAnnotations;

namespace Taskify.API.Contracts.Users;

public record LoginUserRequest(
    [Required] string Email,
    [Required] string Password);
