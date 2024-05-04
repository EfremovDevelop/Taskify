namespace Taskify.API.Contracts.Issues;

public record IssuesRequest(Guid Id,
    string Name, string Description, float TimeSpent,
        int StatusId, Guid ProjectId, Guid? AssignedId);

