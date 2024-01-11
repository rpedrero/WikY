using WikY.Business.Contracts;
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
    }
}
