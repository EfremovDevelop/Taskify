using System.ComponentModel.DataAnnotations;
using Taskify.Core.Models;

namespace Taskify.DataAccess.Entities
{
    public class ProjectEntity
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(Project.MAX_NAME_LENGTH)]
        public string Name { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual ICollection<IssueEntity> Issues { get; set; } = [];
        public virtual ICollection<UserEntity> Users { get; set; } = [];
    }
}
