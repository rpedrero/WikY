using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WikY.Models.Article
{
    public class ArticleEditViewModel
    {
        [Remote("CheckTopicUnicity", "Article", AdditionalFields = nameof(Topic), ErrorMessage = "This topic is already used for another article.")]
        public int Id { get; set; }

        [Required]
        [Remote("CheckTopicUnicity", "Article", AdditionalFields = nameof(Id), ErrorMessage = "This topic is already used for another article.")]
        public string Topic { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string Author { get; set; } = string.Empty;

        [Required]
        [MaxLength(10000)]
        public string Content { get; set; } = string.Empty;
    }
}
