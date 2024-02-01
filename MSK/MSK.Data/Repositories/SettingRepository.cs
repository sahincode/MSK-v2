using MSK.Core.Models;
using MSK.Core.Repositories;
using MSK.Data.DAL;

namespace MSK.Data.Repositories
{
    public class SettingRepository : GenericRepository<Setting>, ISettingRepository
    {
        public SettingRepository(AppDbContext context) : base(context)
        {
        }
    }
}
