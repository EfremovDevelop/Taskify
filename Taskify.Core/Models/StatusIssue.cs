namespace Taskify.Core.Models
{
    public class StatusIssue
    {
        private StatusIssue(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }
        public string Name { get; }

        public static (StatusIssue Status, string Error) Create(int id, string name)
        {
            //нужна валидация
            string error = string.Empty;

            var issue = new StatusIssue(id, name);

            return (issue, error);
        }
    }
}
