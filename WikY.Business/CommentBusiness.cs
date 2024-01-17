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

        public async Task CreateCommentAsync(Comment comment)
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

            await _commentRepository.CreateAsync(comment);
        }

        public async Task<Comment?> GetCommentByIdAsync(int id)
        {
            return await _commentRepository.GetByIdAsync(id);
        }

        public async Task DeleteCommentAsync(Comment comment)
        {
            await _commentRepository.DeleteAsync(comment);
        }
    }
}
