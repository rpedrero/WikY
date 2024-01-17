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

        public async Task<Article?> GetArticleByIdAsync(int id)
        {
            return await _articleRepository.GetByIdAsync(id);
        }

        public async Task<Article?> GetArticleByTopicAsync(string topic)
        {
            return await _articleRepository.GetByTopicAsync(topic);
        }

        public async Task<Article?> GetLastArticleAsync()
        {
            return await _articleRepository.GetLastAsync();
        }

        public IAsyncEnumerable<Article> FindArticle(string? topic, string? content, string? author)
        {
            return _articleRepository.Find(topic, content, author);
        }

        public async Task<bool> ExistsArticleWithTopicAsync(string topic)
        {
            return (await _articleRepository.GetByTopicAsync(topic)) is not null;
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

            if (checkTopicUnicity && await ExistsArticleWithTopicAsync(article.Topic))
            {
                throw new DataValidationException($"This topic is already used for another article.", nameof(article.Topic));
            }

            if (string.IsNullOrWhiteSpace(article.Content))
            {
                throw new DataValidationException("Content is required.", nameof(article.Content));
            }
        }

        public async Task<Article> CreateArticleAsync(Article article)
        {
            await ValidateArticle(article);

            article.DateCreated = DateTime.Now;
            article.DateModified = DateTime.Now;

            return await _articleRepository.CreateAsync(article);
        }

        public async Task UpdateArticleAsync(Article article)
        {
            Article? articleInOldState = await GetArticleByIdAsync(article.Id);
            if (articleInOldState is null)
            {
                throw new ArticleNotFoundException(article.Id);
            }
            
            await ValidateArticle(article, article.Topic != articleInOldState.Topic);

            article.DateModified = DateTime.Now;

            await _articleRepository.UpdateAsync(articleInOldState, article);
        }

        public async Task DeleteArticleAsync(Article article)
        {
            await _articleRepository.DeleteAsync(article);
        }
    }
}
