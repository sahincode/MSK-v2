using MSK.Core.Models;
using MSK.Core.Repositories;
using MSK.Data.DAL;

namespace MSK.Data.Repositories
{
    public class VoteRepository : GenericRepository<Vote>, IVoteRepository
    {
        public VoteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
