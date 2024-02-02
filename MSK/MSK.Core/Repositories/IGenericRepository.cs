using Microsoft.EntityFrameworkCore;
using MSK.Core.Models;
using System.Linq.Expressions;
namespace MSK.Core.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity, new()
    {
        public DbSet<T> Table { get; }
        public Task CreateAsync(T entity);
        public void Delete(T entity);
        public Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? expression, params string[]? includes);
        public Task<T> Get(Expression<Func<T, bool>>? expression, params string[]? includes);
        public Task<int> CommitAsync();


    }
}
