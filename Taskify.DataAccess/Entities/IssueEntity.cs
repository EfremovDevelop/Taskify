using System.ComponentModel.DataAnnotations;
using Taskify.Core.Models;

namespace Taskify.DataAccess.Entities
{
    public class IssueEntity
    {
        public int Id { get; set; }

        [StringLength(Issue.MAX_NAME_LENGTH)]
        public string Name { get; set; }

        public string Description { get; set; } = "";

        public float TimeSpent { get; set; } = 0;

        public int ProjectId { get; set; }

        public StatusEntity Status { get; set; } = StatusEntity.New;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public virtual ProjectEntity Project { get; set; }
    }
}
