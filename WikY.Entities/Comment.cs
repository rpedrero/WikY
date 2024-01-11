using System.ComponentModel.DataAnnotations;

namespace WikY.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(30)]
        public string Author { get; set; } = "";

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        [Required]
        [MaxLength(100)]
        public DateTime Content { get; set; }

        public Article? Article { get; set; }
    }
}
