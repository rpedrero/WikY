using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikY.Entities;
using WikY.Repositories.Contracts;

namespace WikY.Repositories
{
    public class ArticleDbRepository : DbRepository<Article, int>, IArticleRepository
    {
        public ArticleDbRepository(WikYContext dbContext) : base(dbContext) { }

        public override async Task<Article?> GetById(int id)
        {
            return await _dbSet.Where(a => a.Id == id).Include(a => a.Comments).FirstOrDefaultAsync();
        }

        public async Task<Article?> GetByTopic(string topic)
        {
            return await _dbSet.FirstOrDefaultAsync(a => a.Topic == topic);
        }

        public async Task<Article?> GetLast()
        {
            return await _dbSet.OrderByDescending(a => a.DateCreated).FirstOrDefaultAsync();
        }
    }
}
