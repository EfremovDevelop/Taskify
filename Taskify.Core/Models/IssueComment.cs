using System.Runtime.InteropServices;

namespace Taskify.Core.Models;

public class IssueComment
{
    private IssueComment(int id, string comment, DateTime createdDate, Guid userId, Guid issueId)
    {
        Id = id;
        Comment = comment;
        CreatedDate = createdDate;
        UserId = userId;
        IssueId = issueId;
    }
    private IssueComment(string comment, DateTime createdDate, Guid userId, Guid issueId)
    {
        Comment = comment;
        CreatedDate = createdDate;
        UserId = userId;
        IssueId = issueId;
    }
    public int Id { get; set; }

    public string Comment { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public Guid UserId { get; set; }

    public Guid IssueId { get; set; }

    public static (IssueComment IssueComment, string Error) Create(int id, string comment, DateTime createdDate, Guid userId, Guid issueId)
    {
        string error = string.Empty;

        IssueComment issueComment = new IssueComment(id, comment, createdDate, userId, issueId);

        return (issueComment, error);
    }

    public static (IssueComment IssueComment, string Error) Create(string comment, DateTime createdDate, Guid userId, Guid issueId)
    {
        string error = string.Empty;

        IssueComment issueComment = new IssueComment(comment, createdDate, userId, issueId);

        return (issueComment, error);
    }
}
