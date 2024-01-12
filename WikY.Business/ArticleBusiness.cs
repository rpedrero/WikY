using WikY.Business.Contracts;
using WikY.Business.Exceptions;
using WikY.Entities;
using WikY.Repositories.Contracts;

namespace WikY.Business
{
    public class ArticleBusiness : IArticleBusiness
    {
        private IArticleRepository _articleRepository;
        
        public ArticleBusiness(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public IAsyncEnumerable<Article> GetAllArticles()
        {
            return _articleRepository.GetAll();
        }

        public async Task<Article> GetArticleById(int id)
        {
            Article? article = await _articleRepository.GetById(id);
            if(article is not null)
            {
                return article;
            }
            else
            {
                throw new ArticleNotFoundException(id); 
            }
        }

        public async Task<bool> ExistsArticleWithTopic(string topic)
        {
            return (await _articleRepository.GetByTopic(topic)) is not null;
        }

        public async Task<Article> CreateArticle(Article article)
        {
            if (string.IsNullOrWhiteSpace(article.Author))
            {
                throw new DataValidationException("Author is required.");
            }

            if (article.Author.Length > 30)
            {
                throw new DataValidationException("Author must have a maximum length of 30.");
            }

            if (string.IsNullOrWhiteSpace(article.Topic))
            {
                throw new DataValidationException("Topic is required.");
            }

            if (await ExistsArticleWithTopic(article.Topic))
            {
                throw new DataValidationException($"Topic \"{article.Topic}\" is already used.");
            }

            if (string.IsNullOrWhiteSpace(article.Content))
            {
                throw new DataValidationException("Content is required.");
            }

            return await _articleRepository.Create(article);
        }
    }
}
