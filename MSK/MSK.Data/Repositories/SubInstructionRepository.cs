using MSK.Core.Models;
using MSK.Core.Repositories;
using MSK.Data.DAL;

namespace MSK.Data.Repositories
{
    public class SubInstructionRepository : GenericRepository<SubInstruction>, ISubInstructionRepository
    {
        public SubInstructionRepository(AppDbContext context) : base(context){}
    }
}
