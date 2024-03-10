using System.ComponentModel.DataAnnotations;

namespace Taskify.DataAccess.Entities
{
    public class Issue
    {
        public int Id { get; set; }

        [StringLength(Taskify.Core.Models.Issue.MAX_NAME_LENGTH)]
        public string Name { get; set; }

        public string Description { get; set; } = "";

        public float TimeSpent { get; set; } = 0;

        public int ProjectId { get; set; }

        public Status Status { get; set; } = Status.New;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public virtual Project Project { get; set; }
    }
}
