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
    public interface IUserService
    {
       
        Task<User> Get(Expression<Func<User, bool>>? predicate, params string[]? includes);
        Task<IEnumerable<User>> GetAll(Expression<Func<User, bool>>? predicate, params string[]? include);
    }
}
