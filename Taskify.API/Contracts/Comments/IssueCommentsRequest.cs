namespace Taskify.API.Contracts.Comments;

public record IssueCommentsRequest(Guid IssueId, string Comment);
