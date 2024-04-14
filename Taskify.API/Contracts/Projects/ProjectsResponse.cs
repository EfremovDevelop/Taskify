namespace Taskify.API.Contracts.Projects;

public record ProjectsResponse(
    Guid Id,
    string Name,
    string Description,
    DateTime CreatedDate);