using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using WikY.Entities;
using WikY.Repositories.Contracts;

namespace WikY.Repositories
{
    public class ArticleDbRepository : DbRepository<Article, int>, IArticleRepository
    {
        public ArticleDbRepository(WikYContext dbContext) : base(dbContext) { }

        public override async Task<Article?> GetByIdAsync(int id)
        {
            return await _dbSet.Where(a => a.Id == id).Include(a => a.Comments).FirstOrDefaultAsync();
        }

        public async Task<Article?> GetByTopicAsync(string topic)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(a => a.Topic == topic);
        }

        public async Task<Article?> GetLastAsync()
        {
            return await _dbSet.OrderByDescending(a => a.DateCreated).FirstOrDefaultAsync();
        }

        public IAsyncEnumerable<Article> Find(string? topic, string? content, string? author)
        {
            IQueryable<Article> results = _dbSet.Where(a => EF.Functions.Like(a.Topic, $"%{topic}%")
                                                            && EF.Functions.Like(a.Content, $"%{content}%")
                                                            && EF.Functions.Like(a.Author, $"%{author}%"));

            return results.AsAsyncEnumerable();
        }

        public async Task<bool> UpdateAsync(int id, string? topic = null, string? content = null, string? author = null, DateTime? dateCreated = null, DateTime? dateModified = null)
        {
            int result = await _dbSet.Where(a => a.Id == id).ExecuteUpdateAsync(s => s.SetProperty(a => a.Topic, a => topic ?? a.Topic)
                                                                                      .SetProperty(a => a.Content, a => content ?? a.Content)
                                                                                      .SetProperty(a => a.Author, a => author ?? a.Author)
                                                                                      .SetProperty(a => a.DateCreated, a => dateCreated ?? a.DateCreated)
                                                                                      .SetProperty(a => a.DateModified, a => dateModified ?? a.DateModified));

            return result > 0;
        }
    }
}
