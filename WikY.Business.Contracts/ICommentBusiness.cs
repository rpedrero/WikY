using WikY.Entities;

namespace WikY.Business.Contracts
{
    public interface ICommentBusiness
    {
        public Task CreateCommentAsync(Comment comment);
        public Task<Comment?> GetCommentByIdAsync(int id);
        public Task DeleteCommentAsync(Comment comment);
    }
}
