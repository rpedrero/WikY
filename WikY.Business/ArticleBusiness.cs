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
    }
}
