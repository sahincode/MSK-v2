using Microsoft.EntityFrameworkCore;
using MSK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Core.Repositories
{
    public  interface IUserRepository
    {
        public DbSet<User> Table { get; }
        public void Delete(User entity);
        public Task<IEnumerable<User>> GetAll(Expression<Func<User, bool>>? expression, params string[]? includes);
        public Task<User> Get(Expression<Func<User, bool>>? expression, params string[]? includes);
        public Task<int> CommitAsync();
    }
}
