using WikY.Entities;

namespace WikY.Business.Contracts
{
    public interface IArticleBusiness
    {
        public IAsyncEnumerable<Article> GetAllArticles();
    }
}
