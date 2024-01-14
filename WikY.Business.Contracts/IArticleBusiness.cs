using WikY.Entities;

namespace WikY.Business.Contracts
{
    public interface IArticleBusiness
    {
        public IAsyncEnumerable<Article> GetAllArticles();
        public Task<Article?> GetArticleById(int id);
        public Task<Article?> GetArticleByTopic(string topic);
        public Task<bool> ExistsArticleWithTopic(string topic);
        public Task<Article> CreateArticle(Article article);
        Task UpdateArticle(Article article);
    }
}
