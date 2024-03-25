using Taskify.Core.Interfaces.Repositories;
using Taskify.DataAccess.Entities;

namespace Taskify.DataAccess.Repositories
{
    public class IssueStatusesRepository : IIssueStatusesRepository
    {
        public List<string> GetList()
        {
            return Enum.GetNames(typeof(StatusEntity)).ToList();
        }
    }
}
