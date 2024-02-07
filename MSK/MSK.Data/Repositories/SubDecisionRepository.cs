using MSK.Core.Models;
using MSK.Core.Repositories;
using MSK.Data.DAL;

namespace MSK.Data.Repositories
{
    public class SubDecisionRepository : GenericRepository<SubDecision>, ISubDecisionRepository
    {
        public SubDecisionRepository(AppDbContext context) : base(context){}
    }
}
