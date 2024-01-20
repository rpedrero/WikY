using System.ComponentModel.DataAnnotations;

namespace WikY.Entities
{
    public class Article
    {
        public int Id { get; set; }

        [Required]
        public string Topic { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string Author { get; set; } = "";

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        [Required]
        [MaxLength(10000)]
        public string Content { get; set; } = string.Empty;

        public ICollection<Comment>? Comments { get; set; }
    }
}
