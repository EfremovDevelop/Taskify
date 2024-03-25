using Taskify.Core.Interfaces.Repositories;
using Taskify.Core.Interfaces.Services;

namespace Taskify.Application.Services
{
    public class IssueStatusesService : IIssueStatusesService
    {
        private readonly IIssueStatusesRepository _issueStatusesRepository;

        public IssueStatusesService(IIssueStatusesRepository issueStatusesRepository)
        {
            _issueStatusesRepository = issueStatusesRepository;
        }

        public List<string> GetIssueStatusesList()
        {
            return _issueStatusesRepository.GetList();
        }
    }
}
