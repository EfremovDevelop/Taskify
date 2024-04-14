namespace Taskify.Core.Models;

public class User
{
    private User(Guid id, string name, string email, string password)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
    }

    public Guid Id { get; }
    public string Name { get; }
    public string Email { get; }
    public string Password { get; }

    public static (User user, string error) Create(Guid id, string name, string email, string password)
    {
        string error = string.Empty;
        if (string.IsNullOrEmpty(email))
            error = "Error create User. Email is empty";
        if (string.IsNullOrEmpty(name))
            error = "Error creat User. Name is empty";
        var user = new User(id, name, email, password);
        return (user, error);
    }
}
