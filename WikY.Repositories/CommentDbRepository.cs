using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikY.Entities;
using WikY.Repositories.Contracts;

namespace WikY.Repositories
{
    public class CommentDbRepository : DbRepository<Comment, int>, ICommentRepository
    {
        public CommentDbRepository(WikYContext dbContext) : base(dbContext) { }
    }
}
