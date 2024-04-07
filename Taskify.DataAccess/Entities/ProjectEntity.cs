using System.ComponentModel.DataAnnotations;
using Taskify.Core.Models;

namespace Taskify.DataAccess.Entities
{
    public class ProjectEntity
    {
        public ProjectEntity()
        {
            Issue = new HashSet<IssueEntity>();
            Users = new HashSet<ProjectUserEntity>();
        }
        public Guid Id { get; set; }

        [Required]
        [StringLength(Project.MAX_NAME_LENGTH)]
        public string Name { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual ICollection<IssueEntity> Issue { get; set;}
        public virtual ICollection<ProjectUserEntity> Users { get; set; }
    }
}
