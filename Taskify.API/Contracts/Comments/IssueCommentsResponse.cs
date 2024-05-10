using Taskify.API.Contracts.Users;

namespace Taskify.API.Contracts.Comments;

public record IssueCommentsResponse(int? Id, string Comment, DateTime CreatedDate, UsersResponse User);
