using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikY.Entities;

namespace WikY.Repositories
{
    public class ArticleDbRepository : DbRepository<Article, int>
    {
        public ArticleDbRepository(WikYContext dbContext) : base(dbContext) { }
    }
}
