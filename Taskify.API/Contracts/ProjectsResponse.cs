namespace Taskify.API.Contracts
{
    public record ProjectsResponse (
        Guid Id,
        string Name,
        string Description,
        DateTime CreatedDate);
}
