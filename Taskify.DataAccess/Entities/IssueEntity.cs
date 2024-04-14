using System.ComponentModel.DataAnnotations;
using Taskify.Core.Models;

namespace Taskify.DataAccess.Entities;

public class IssueEntity
{
    public Guid Id { get; set; }

    [StringLength(Issue.MAX_NAME_LENGTH)]
    public string Name { get; set; }

    public string Description { get; set; } = "";

    public float TimeSpent { get; set; } = 0;

    public Guid ProjectId { get; set; }

    public int StatusId { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public DateTime UpdatedDate { get; set; } = DateTime.Now;

    public int RefId { get; set; }

    public virtual StatusIssueEntity Status { get; set; }

    public virtual ProjectEntity Project { get; set; }
}
