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
    public interface IVoterRepository
    {
        public DbSet<Voter> Table { get; }
        public Task CreateAsync(Voter entity);
        public void Delete(Voter entity);
        public Task<IEnumerable<Voter>> GetAll(Expression<Func<Voter, bool>>? expression, params string[]? includes);
        public Task<Voter> Get(Expression<Func<Voter, bool>>? expression, params string[]? includes);
        public Task<int> CommitAsync();
    }
}
