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

        public async Task<Article?> GetArticleById(int id)
        {
            return await _articleRepository.GetById(id);
        }

        public async Task<Article?> GetArticleByTopic(string topic)
        {
            return await _articleRepository.GetByTopic(topic);
        }

        public async Task<Article?> GetLastArticle()
        {
            return await _articleRepository.GetLast();
        }

        public async Task<bool> ExistsArticleWithTopic(string topic)
        {
            return (await _articleRepository.GetByTopic(topic)) is not null;
        }

        private async Task ValidateArticle(Article article, bool checkTopicUnicity = true)
        {
            if (string.IsNullOrWhiteSpace(article.Author))
            {
                throw new DataValidationException("Author is required.", nameof(article.Author));
            }

            if (article.Author.Length > 30)
            {
                throw new DataValidationException("Author must have a maximum length of 30.", nameof(article.Author));
            }

            if (string.IsNullOrWhiteSpace(article.Topic))
            {
                throw new DataValidationException("Topic is required.", nameof(article.Topic));
            }

            if (checkTopicUnicity && await ExistsArticleWithTopic(article.Topic))
            {
                throw new DataValidationException($"This topic is already used for another article.", nameof(article.Topic));
            }

            if (string.IsNullOrWhiteSpace(article.Content))
            {
                throw new DataValidationException("Content is required.", nameof(article.Content));
            }
        }

        public async Task<Article> CreateArticle(Article article)
        {
            await ValidateArticle(article);

            article.DateCreated = DateTime.Now;
            article.DateModified = DateTime.Now;

            return await _articleRepository.Create(article);
        }

        public async Task UpdateArticle(Article article)
        {
            Article? articleInOldState = await GetArticleById(article.Id);
            if (articleInOldState is null)
            {
                throw new ArticleNotFoundException(article.Id);
            }
            
            await ValidateArticle(article, article.Topic != articleInOldState.Topic);

            article.DateModified = DateTime.Now;

            await _articleRepository.Update(articleInOldState, article);
        }

        public async Task DeleteArticle(Article article)
        {
            await _articleRepository.Delete(article);
        }
    }
}
