using WikY.Entities;

namespace WikY.Business.Contracts
{
    public interface ICommentBusiness
    {
        public Task CreateComment(Comment comment);
    }
}
