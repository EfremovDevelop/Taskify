using System.ComponentModel.DataAnnotations;

namespace Taskify.DataAccess.Entities
{
    public class Project
    {
        public Project()
        {
            Issue = new HashSet<Issue>();
        }
        public int Id { get; set; }

        [Required]
        [StringLength(Taskify.Core.Models.Project.MAX_NAME_LENGTH)]
        public string Name { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Issue> Issue { get; set;}
    }
}
