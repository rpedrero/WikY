using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WikY.Entities;

namespace WikY.Models
{
    public class ArticleViewModel
    {
        private Article _article;

        public int Id
        {
            get
            {
                return _article.Id;
            }
            set
            {
                _article.Id = value;
            }
        }

        [Required]
        [Remote("CheckTopicUnicity", "Article", ErrorMessage = "This topic is already used for another article.")]
        public string Topic
        {
            get
            {
                return _article.Topic;
            }
            set
            {
                _article.Topic = value;
            }
        }

        [Required]
        [MaxLength(30)]
        public string Author
        {
            get
            {
                return _article.Author;
            }
            set
            {
                _article.Author = value;
            }
        }

        public DateTime DateCreated
        {
            get
            {
                return _article.DateCreated;
            }
            set
            {
                _article.DateCreated = value;
            }
        }

        public DateTime DateModified
        {
            get
            {
                return _article.DateModified;
            }
            set
            {
                _article.DateModified = value;
            }
        }

        [Required]
        public string Content
        {
            get
            {
                return _article.Content;
            }
            set
            {
                _article.Content = value;
            }
        }

        public ICollection<Comment>? Comments
        {
            get
            {
                return _article.Comments;
            }
            set
            {
                _article.Comments = value;
            }
        }

        public ArticleViewModel(Article article)
        {
            _article = article;
        }

        public ArticleViewModel() : this(new Article())
        {
        }

        public Article GetArticle()
        {
            return _article;
        }
    }
}
