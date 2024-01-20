using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WikY.Models.Comment;

namespace WikY.Models.Article
{
    public class ArticleCreateViewModel
    {
        [Required]
        [Remote("CheckTopicUnicity", "Article", ErrorMessage = "This topic is already used for another article.")]
        public string Topic { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string Author { get; set; } = string.Empty;

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        [Required]
        [MaxLength(10000)]
        public string Content { get; set; } = string.Empty;

        public ICollection<CommentViewModel>? Comments { get; set; }
    }
}
