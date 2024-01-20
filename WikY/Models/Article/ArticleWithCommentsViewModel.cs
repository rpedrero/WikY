using WikY.Models.Comment;

namespace WikY.Models.Article
{
    public class ArticleWithCommentsViewModel : ArticleViewModel
    {
        public ICollection<CommentViewModel> Comments { get; set; } = new List<CommentViewModel>();
    }
}
