using WikY.Entities;

namespace WikY.Business.Contracts
{
    public interface ICommentBusiness
    {
        public Task CreateComment(Comment comment);
        public Task<Comment?> GetCommentById(int id);
        public Task DeleteComment(Comment comment);
    }
}
