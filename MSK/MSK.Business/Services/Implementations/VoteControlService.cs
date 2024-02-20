using Microsoft.AspNetCore.Http;
using MSK.Core.Repositories;

namespace MSK.Business.Services.Implementations
{
    public class VoteControlService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IVoteRepository _voteRepository;

        public VoteControlService(IHttpContextAccessor httpContext, IVoteRepository voteRepository)
        {
            this._httpContext = httpContext;
            this._voteRepository = voteRepository;
        }
        public bool Voted()
        {
            var finCode = _httpContext?.HttpContext?.User?.Identity?.Name; // You may need to adjust this depending on your authentication setup
            var existingVote = _voteRepository.Table.FirstOrDefault(v => v.VoterFinCode == finCode);
            if (existingVote is null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public string CheckCandidate(int canId)
        {
            var finCode = _httpContext?.HttpContext?.User?.Identity?.Name; // You may need to adjust this depending on your authentication setup
            var existingVote = _voteRepository.Table.FirstOrDefault(v => v.VoterFinCode == finCode);
            if (existingVote is not  null)
            {
                if (existingVote.CandidateId == canId)
                    return "checked";
                else
                    return "";
            }
            else
            {
                return "";
            }
        }
    }
}
