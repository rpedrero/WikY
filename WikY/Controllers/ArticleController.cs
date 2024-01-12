using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using WikY.Business.Contracts;
using WikY.Business.Exceptions;
using WikY.Entities;
using WikY.Models;

namespace WikY.Controllers
{
    public class ArticleController : Controller
    {
        private ILogger<ArticleController> _log;
        private IArticleBusiness _articleBusiness;

        public ArticleController(ILogger<ArticleController> log, IArticleBusiness articleBusiness)
        {
            _log = log;
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ArticleViewModel article)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    Article createdArticle = await _articleBusiness.CreateArticle(article.GetArticle());

                    TempData["success"] = "The article has successfully been created.";

                    return RedirectToAction("View", new { createdArticle.Id });
                }
                catch(DataValidationException ex)
                {
                    _log.LogError(ex.Message);

                    TempData["error"] = ex.Message;

                    return View(article);
                }
                catch(Exception ex)
                {
                    _log.LogError(ex.Message);

                    TempData["error"] = "An error occured when attempting to create article. Try again later.";
                    
                    return View(article);
                }
            }
            else
            {
                return View(article);
            }
        }

        public async Task<IActionResult> CheckTopicUnicity(string topic)
        {
            return Json(!await _articleBusiness.ExistsArticleWithTopic(topic));
        }
    }
}
