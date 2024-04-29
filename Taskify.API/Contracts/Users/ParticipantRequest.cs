namespace Taskify.API.Contracts.Users;

public record ParticipantRequest(string Email, Guid ProjectId);
