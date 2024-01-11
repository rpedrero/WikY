using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikY.Entities;

namespace WikY.Repositories
{
    public class CommentDbRepository : DbRepository<Article, int>
    {
        public CommentDbRepository(WikYContext dbContext) : base(dbContext) { }
    }
}
