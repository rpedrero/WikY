using System.ComponentModel.DataAnnotations;
using WikY.Entities;

namespace WikY.Models
{
    public class CommentViewModel
    {
        private Comment _comment;

        public CommentViewModel(Comment comment)
        {
            _comment = comment;
        }

        public int Id
        {
            get
            {
                return _comment.Id;
            }
            set
            {
                _comment.Id = value;
            }
        }

        [Required]
        [MaxLength(30)]
        public string Author
        {
            get
            {
                return _comment.Author;
            }
            set
            {
                _comment.Author = value;
            }
        }

        public DateTime DateCreated
        {
            get
            {
                return _comment.DateCreated;
            }
            set
            {
                _comment.DateCreated = value;
            }
        }

        public DateTime DateModified
        {
            get
            {
                return _comment.DateModified;
            }
            set
            {
                _comment.DateModified = value;
            }
        }

        [Required]
        [MaxLength(100)]
        public DateTime Content
        {
            get
            {
                return _comment.Content;
            }
            set
            {
                _comment.Content = value;
            }
        }

        public Article? Article
        {
            get
            {
                return _comment.Article;
            }
            set
            {
                _comment.Article = value;
            }
        }
    }
}
