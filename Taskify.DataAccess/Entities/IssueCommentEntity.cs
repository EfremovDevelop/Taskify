namespace Taskify.DataAccess.Entities;

public class IssueCommentEntity
{
    public int Id { get; set; }

    public string Comment {  get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public Guid UserId { get; set; }

    public Guid IssueId {  get; set; }

    public virtual IssueEntity Issue { get; set; }

    public virtual UserEntity User { get; set; }
}