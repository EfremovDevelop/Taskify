namespace Taskify.Core.Models;

public class Project
{
    public const int MAX_NAME_LENGTH = 100;

    private Project(Guid id, string name, string description, DateTime createdDate) 
    {
        Id = id;
        Name = name;
        Description = description;
        CreatedDate = createdDate;
    }

    public Guid Id { get; }
    public string Name { get; } = string.Empty;
    public string Description { get; } = string.Empty;
    public DateTime CreatedDate { get; }

    public static (Project Project, string Error) Create(Guid id, string Name, string Description, DateTime CreatedDate)
    {
        string error = string.Empty;
        if (Name.Length >= MAX_NAME_LENGTH)
            error = "Error. Name can not be longer then ${MAX_NAME_LENGTH} symbols";
        else if (string.IsNullOrEmpty(Name))
            error = "Error. Name can not be is empty";

        Project project = new Project(id, Name, Description, CreatedDate);
        return (project, error);
    }
}
