using Taskify.Core.Enums;

namespace Taskify.Core.Models
{
    public class Issue
    {
        public const int MAX_NAME_LENGTH = 100;
        private Issue(Guid id, string name, string description, float timeSpent, Guid projectId,
            int statusId, DateTime createdDate, DateTime updateDate, int refId) 
        {
            Id = id;
            Name = name;
            Description = description;
            TimeSpent = timeSpent;
            ProjectId = projectId;
            StatusId = statusId;
            CreatedDate = createdDate;
            UpdatedDate = updateDate;
            RefId = refId;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; } = string.Empty;
        public float TimeSpent { get; } = 0;
        public Guid ProjectId { get; }
        public int StatusId { get; }
        public int RefId { get; }

        public DateTime CreatedDate { get; }
        public DateTime UpdatedDate { get; }

        public static (Issue Issue, string Error) Create(Guid id, string name, string description, float timeSpent, Guid projectId,
            int statusId, DateTime createdDate, DateTime updateDate, int RefId)
        {
            string error = string.Empty;
            if (string.IsNullOrEmpty(name))
                error = "Error. Name can not be is empty";
            else if (name.Length >= MAX_NAME_LENGTH)
                error = $"Error. Name can not be longer then {MAX_NAME_LENGTH} symbols";
            else if (timeSpent < 0)
                error = "Error. TimeSpent can not be < 0";

            Issue issue = new Issue(id, name, description, timeSpent, projectId, statusId, createdDate, updateDate, RefId);

            return (issue, error);
        }
    }
}
