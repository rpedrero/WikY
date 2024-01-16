using WikY.Entities;

namespace WikY.Business.Contracts
{
    public interface IArticleBusiness
    {
        IAsyncEnumerable<Article> GetAllArticles();
        Task<Article?> GetArticleById(int id);
        Task<Article?> GetArticleByTopic(string topic);
        Task<Article?> GetLastArticle();
        IAsyncEnumerable<Article> FindArticle(string? topic, string? content, string? author);
        Task<bool> ExistsArticleWithTopic(string topic);
        Task<Article> CreateArticle(Article article);
        Task UpdateArticle(Article article);
        Task DeleteArticle(Article article);
    }
}
