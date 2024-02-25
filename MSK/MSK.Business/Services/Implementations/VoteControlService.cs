using Microsoft.AspNetCore.Http;
using MSK.Core.enums;
using MSK.Core.Models;
using MSK.Core.Repositories;

namespace MSK.Business.Services.Implementations
{
    public class VoteControlService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IVoteRepository _voteRepository;
        private readonly IElectionRepository _electionRepository;

        public VoteControlService(IHttpContextAccessor httpContext,
            IVoteRepository voteRepository, IElectionRepository electionRepository)
        {
            this._httpContext = httpContext;
            this._voteRepository = voteRepository;
            this._electionRepository = electionRepository;
        }
        public async Task<bool> Voted(int id)
        {
            var voteElection = await _electionRepository.Get(e => e.Id==id);
            var finCode = _httpContext?.HttpContext?.User?.Identity?.Name; // You may need to adjust this depending on your authentication setup
            var existingVotes = _voteRepository.Table.Where(v => v.VoterFinCode == finCode).ToList();
            if (existingVotes.Any(ev => voteElection.Candidates.Any(c => c.Id == ev.CandidateId)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> CheckCandidate(int canId)
        {
            var finCode = _httpContext?.HttpContext?.User?.Identity?.Name; // You may need to adjust this depending on your authentication setup
            var existingVotes = _voteRepository.Table.Where(v => v.VoterFinCode == finCode).ToList();

            if (existingVotes.Any(ev =>canId == ev.CandidateId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
