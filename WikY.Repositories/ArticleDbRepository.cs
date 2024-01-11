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
    }
}
