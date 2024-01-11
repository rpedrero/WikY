using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using WikY.Entities;
using WikY.Repositories.Contracts;

namespace WikY.Repositories
{
    public class DbRepository<T, ID> : IRepository<T, ID> where T : class
    {
        private readonly WikYContext _context;

        protected readonly DbSet<T> _dbSet;

        public DbRepository(WikYContext dbContext)
        {
            _context = dbContext;

            _dbSet = _context.Set<T>();
        }

        public async Task<T> Create(T entity)
        {
            await _dbSet.AddAsync(entity);

            await _context.SaveChangesAsync();

            return await Task.FromResult(entity);
        }

        public async Task Delete(T entity)
        {
            _dbSet.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public IAsyncEnumerable<T> GetAll()
        {
            return _dbSet.AsAsyncEnumerable();
        }

        public async Task<T?> GetById(ID id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Update(T entity)
        {
            _dbSet.Update(entity);

            await _context.SaveChangesAsync();
        }
    }
}
