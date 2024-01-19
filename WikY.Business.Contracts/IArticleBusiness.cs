using WikY.Entities;

namespace WikY.Business.Contracts
{
    public interface IArticleBusiness
    {
        IAsyncEnumerable<Article> GetAllArticles();
        Task<Article?> GetArticleByIdAsync(int id);
        Task<Article?> GetArticleByTopicAsync(string topic);
        Task<Article?> GetLastArticleAsync();
        IAsyncEnumerable<Article> FindArticle(string? topic, string? content, string? author);
        Task<bool> CheckArticleTopicUnicity(string articleTopic, int articleid = default);
        Task<Article> CreateArticleAsync(Article article);
        Task UpdateArticleAsync(Article article);
        Task DeleteArticleAsync(Article article);
    }
}
