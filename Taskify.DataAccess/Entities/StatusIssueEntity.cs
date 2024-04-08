namespace Taskify.DataAccess.Entities
{
    public class StatusIssueEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<IssueEntity> Issues { get; set; } = [];
    }
}
