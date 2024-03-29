﻿using System.ComponentModel.DataAnnotations;

namespace WikY.Models.Comment
{
    public class CommentCreateViewModel
    {
        [Required]
        [MaxLength(30)]
        public string Author { get; set; } = string.Empty;

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Content { get; set; } = string.Empty;

        public int ArticleId { get; set; }
    }
}
