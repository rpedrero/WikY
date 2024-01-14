using Microsoft.AspNetCore.Mvc;
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
            Article? article = await _articleBusiness.GetArticleById(id);

            if(article is not null)
            {
                return View(new ArticleViewModel(article));
            }
            else
            {
                TempData["error"] = "Article not found.";
                
                return RedirectToAction("Index");
            }
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
                Article createdArticle = article.GetArticle();
                try
                {
                    await _articleBusiness.CreateArticle(article.GetArticle());
                }
                catch(DataValidationException ex)
                {
                    ModelState.AddModelError(ex.FieldName, ex.Message);

                    return View(article);
                }
                catch(Exception ex)
                {
                    _log.LogError(ex.Message);

                    TempData["error"] = "An error occurred when attempting to create article. Try again later.";
                    
                    return View(article);
                }

                TempData["success"] = "The article has been successfully created.";

                return RedirectToAction("View", new { createdArticle.Id });
            }
            else
            {
                return View(article);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            Article? article = await _articleBusiness.GetArticleById(id);

            if (article is not null)
            {
                return View(new ArticleViewModel(article));
            }
            else
            {
                TempData["error"] = "The article could not be found.";

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ArticleViewModel article)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _articleBusiness.UpdateArticle(article.GetArticle());
                }
                catch (ArticleNotFoundException)
                {
                    TempData["error"] = "Specified article does not exist.";

                    return RedirectToAction("Index");
                }
                catch (DataValidationException ex)
                {
                    ModelState.AddModelError(ex.FieldName, ex.Message);

                    return View(article);
                }
                catch (Exception ex)
                {
                    _log.LogError(ex.Message);

                    TempData["error"] = "An error occurred when attempting to update the article. Try again later.";

                    return View(article);
                }

                TempData["success"] = "The article has been successfully updated.";

                return RedirectToAction("View", new { article.Id });
            }
            else
            {
                return View(article);
            }
        }

        public async Task<IActionResult> CheckTopicUnicity(int id, string topic)
        {
            Article? article = await _articleBusiness.GetArticleByTopic(topic);

            return Json(article is null || (id != default && article.Id == id));
        }
    }
}
