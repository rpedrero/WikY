using System.ComponentModel.DataAnnotations;

namespace WikY.Entities
{
    public class Comment
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
        [Required]
        public int ArticleId { get; set; }
        public Article? Article { get; set; }
    }
}
