namespace Taskify.API.Contracts.Issues;

public record IssuesResponse(Guid Id,
    string Name, string Description, float TimeSpent,
        int StatusId, DateTime CreatedDate, DateTime UpdateDate, int RefId);

