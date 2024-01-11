using WikY.Entities;

namespace WikY.Business.Contracts
{
    public interface IArticleBusiness
    {
        public IAsyncEnumerable<Article> GetAllArticles();
        public Task<Article> GetArticleById(int id);
        public Task<bool> ExistsArticleWithTopic(string topic);
    }
}
