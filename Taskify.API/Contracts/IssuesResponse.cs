using Taskify.Core.Models;

namespace Taskify.API.Contracts
{
	public record IssuesResponse(
        string Name, string Description, float TimeSpent,
            Status Status, DateTime CreatedDate, DateTime UpdateDate, int PathId);
}

