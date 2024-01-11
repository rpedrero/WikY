using Microsoft.AspNetCore.Mvc;
using WikY.Business.Contracts;
using WikY.Business.Exceptions;
using WikY.Entities;
using WikY.Models;

namespace WikY.Controllers
{
    public class ArticleController : Controller
    {
        private IArticleBusiness _articleBusiness;

        public ArticleController(IArticleBusiness articleBusiness)
        {
            _articleBusiness = articleBusiness;
        }

        public async Task<IActionResult> Index()
        {
            IAsyncEnumerable<Article> articles = _articleBusiness.GetAllArticles();
            IList<ArticleViewModel> articlesViewModels = new List<ArticleViewModel>();

            await foreach (Article article in articles)
            {
                articlesViewModels.Add(new ArticleViewModel(article));
            }

            return View(articlesViewModels);
        }

        public async Task<IActionResult> View(int id)
        {
            Article article;

            try
            {
                article = await _articleBusiness.GetArticleById(id);
            }
            catch (ArticleNotFoundException)
            {
                return NotFound();
            }

            return View(new ArticleViewModel(article));
        }
    }
}
