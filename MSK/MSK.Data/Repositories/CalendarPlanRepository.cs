using MSK.Core.Models;
using MSK.Core.Repositories;
using MSK.Data.DAL;
namespace MSK.Data.Repositories
{
    public class CalendarPlanRepository : GenericRepository<CalendarPlan>, ICalendarPlanRepository
    {
        public CalendarPlanRepository(AppDbContext context) : base(context){}
    }
}
