namespace Taskify.Core.Models
{
    public class Issue
    {
        public const int MAX_NAME_LENGTH = 100;
        private Issue(int id, string name, string description, float timeSpent, int projectId,
            Status status, DateTime createdDate, DateTime updateDate) 
        {
            Id = id;
            Name = name;
            Description = description;
            TimeSpent = timeSpent;
            ProjectId = projectId;
            Status = status;
            CreatedDate = createdDate;
            UpdatedDate = updateDate;
        }

        public int Id { get; }
        public string Name { get; }
        public string Description { get; } = string.Empty;
        public float TimeSpent { get; } = 0;
        public int ProjectId { get; }
        public Status Status { get; }

        public DateTime CreatedDate { get; }
        public DateTime UpdatedDate { get; }

        public static (Issue issue, string error) Create(int id, string name, string description, float timeSpent, int projectId,
            Status status, DateTime createdDate, DateTime updateDate)
        {
            string error = string.Empty;
            if (string.IsNullOrEmpty(name))
                error = "Error. Name can not be is empty";
            else if (name.Length >= MAX_NAME_LENGTH)
                error = $"Error. Name can not be longer then {MAX_NAME_LENGTH} symbols";
            else if (timeSpent < 0)
                error = "Error. TimeSpent can not be < 0";

            Issue issue = new Issue(id, name, description, timeSpent, projectId, status, createdDate, updateDate);

            return (issue, error);
        }
    }
}
