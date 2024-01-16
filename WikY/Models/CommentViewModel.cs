using System.ComponentModel.DataAnnotations;
using WikY.Entities;

namespace WikY.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Author { get; set; } = string.Empty;

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        [Required]
        [MaxLength(100)]
        public string Content { get; set; } = string.Empty;

        public int ArticleId { get; set; }
    }
}
