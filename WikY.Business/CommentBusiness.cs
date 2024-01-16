using WikY.Business.Contracts;
using WikY.Business.Exceptions;
using WikY.Entities;
using WikY.Repositories.Contracts;

namespace WikY.Business
{
    public class CommentBusiness : ICommentBusiness
    {
        private ICommentRepository _commentRepository;
        
        public CommentBusiness(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task CreateComment(Comment comment)
        {
            if(string.IsNullOrWhiteSpace(comment.Author))
            {
                throw new DataValidationException("Author is required.", nameof(comment.Author));
            }
            
            if(comment.Author.Length > 30)
            {
                throw new DataValidationException("Author must have a maximum length of 30.", nameof(comment.Author));
            }

            comment.DateCreated = DateTime.Now;

            await _commentRepository.Create(comment);
        }

        public async Task<Comment?> GetCommentById(int id)
        {
            return await _commentRepository.GetById(id);
        }

        public async Task DeleteComment(Comment comment)
        {
            await _commentRepository.Delete(comment);
        }
    }
}
