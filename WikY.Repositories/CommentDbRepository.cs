using WikY.Entities;
using WikY.Repositories.Contracts;

namespace WikY.Repositories
{
    public class CommentDbRepository : DbRepository<Comment, int>, ICommentRepository
    {
        public CommentDbRepository(WikYContext dbContext) : base(dbContext) { }
    }
}
