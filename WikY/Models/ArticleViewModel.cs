using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WikY.Entities;

namespace WikY.Models
{
    public class ArticleViewModel
    {
        [Remote("CheckTopicUnicity", "Article", AdditionalFields = nameof(Topic), ErrorMessage = "This topic is already used for another article.")]
        public int Id { get; set; }

        [Required]
        [Remote("CheckTopicUnicity", "Article", AdditionalFields = nameof(Id), ErrorMessage = "This topic is already used for another article.")]
        public string Topic { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string Author { get; set; } = string.Empty;

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        public ICollection<CommentViewModel>? Comments { get; set; }
    }
}
