using Taskify.Core.Models;

namespace Taskify.API.Contracts
{
	public record IssuesResponse(Guid Id,
        string Name, string Description, float TimeSpent,
            string Status, DateTime CreatedDate, DateTime UpdateDate, int RefId);
}

