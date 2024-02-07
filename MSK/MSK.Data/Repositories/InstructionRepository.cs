using MSK.Core.Models;
using MSK.Core.Repositories;
using MSK.Data.DAL;
namespace MSK.Data.Repositories
{
    public class InstructionRepository : GenericRepository<Instruction>, IInstructionRepository
    {
        public InstructionRepository(AppDbContext context) : base(context){}
    }
}
