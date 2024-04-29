namespace Taskify.Core.Models;

public class ProjectUser
{
    private ProjectUser(Guid id, string name, string email, Role role)
    {
        Id = id;
        Name = name;
        Email = email;
        Role = role;
    }

    public Guid Id { get; }

    public string Name { get; }

    public string Email { get; }

    public Role Role { get; }

    public static (ProjectUser ProjectUser, string Error) Create(Guid id, string name, string email, Role role)
    {
        string error = string.Empty;

        var projectUser = new ProjectUser(id, name, email, role);

        return (projectUser, error);
    }
}

public class Role
{
    private Role(int id, string name)
    {
        Id = id;
        Name = name;
    }
    public int Id { get; }

    public string Name { get; }

    public static (Role Role, string Error) Create(int id, string name)
    {
        string Error = string.Empty;

        var role = new Role(id, name);

        return (role, Error);
    }
}