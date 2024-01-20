using Microsoft.EntityFrameworkCore;
using WikY.Entities;
using WikY.Repositories.Contracts;

namespace WikY.Repositories
{
    public class DbRepository<T, ID> : IRepository<T, ID> where T : class
    {
        protected readonly WikYContext _context;

        protected readonly DbSet<T> _dbSet;

        public DbRepository(WikYContext dbContext)
        {
            _context = dbContext;

            _dbSet = _context.Set<T>();
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);

            await _context.SaveChangesAsync();

            return await Task.FromResult(entity);
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public virtual IAsyncEnumerable<T> GetAll()
        {
            return _dbSet.AsAsyncEnumerable();
        }

        public virtual async Task<T?> GetByIdAsync(ID id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);

            return (await _context.SaveChangesAsync()) > 1;
        }
    }
}
