using MSK.Business.DTOs.CalendarPlanModelDTOs;
using MSK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.Services.Interfaces
{
    public interface ICalendarPlanService
    {
        Task CreateAsync(CalendarPlanCreateDto entity);
        Task UpdateAsync(CalendarPlanUpdateDto entity);
        Task Delete(int id);
        Task ToggleDelete(int id);
        Task<CalendarPlan> GetById(int? id);
        Task<CalendarPlan> Get(Expression<Func<CalendarPlan, bool>>? predicate, params string[]? includes);
        Task<IEnumerable<CalendarPlan>> GetAll(Expression<Func<CalendarPlan, bool>>? predicate, params string[]? include);
    }
}
