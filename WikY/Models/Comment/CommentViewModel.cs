using System.ComponentModel.DataAnnotations;

namespace WikY.Models.Comment
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public string Author { get; set; } = string.Empty;

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string Content { get; set; } = string.Empty;

        public int ArticleId { get; set; }
    }
}
