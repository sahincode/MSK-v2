using Microsoft.EntityFrameworkCore;
using MSK.Core.Models;
using MSK.Core.Repositories;
using MSK.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Data.Repositories
{
    public  class UserRepository :IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            this._context = context;
        }
        public DbSet<User> Table => _context.Set<User>();

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(User entity)
        {
            await Table.AddAsync(entity);
        }

        public void Delete(User entity)
        {
            Table.Remove(entity);
        }

        public async Task<User> Get(Expression<Func<User, bool>>? expression, params string[]? includes)
        {
            var query = this.GetQuery(includes);
            return expression is not null ? await query.Where(expression).FirstOrDefaultAsync() : await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetAll(Expression<Func<User, bool>>? expression, params string[]? includes)
        {
            var query = this.GetQuery(includes);
            return expression is not null ? await query.Where(expression).ToListAsync() : await query.ToListAsync();
        }
        public IQueryable<User> GetQuery(params string[] includes)
        {
            var query = Table.AsQueryable();
            if (includes is not null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query;
        }
    }
}
