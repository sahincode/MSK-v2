using MSK.Core.Models;
using MSK.Core.Repositories;
using MSK.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Data.Repositories
{
    public class PressNewRepository : GenericRepository<PressNew>, IPressNewRepository
    {
        public PressNewRepository(AppDbContext context) : base(context) { }
    }
}
