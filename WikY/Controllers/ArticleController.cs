using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WikY.Business.Contracts;
using WikY.Business.Exceptions;
using WikY.Entities;
using WikY.Models;

namespace WikY.Controllers
{
    public class ArticleController : Controller
    {
        private ILogger<ArticleController> _logger;
        private IArticleBusiness _articleBusiness;
        private IMapper _mapper;

        public ArticleController(ILogger<ArticleController> logger, IArticleBusiness articleBusiness, IMapper mapper)
        {
            _logger = logger;
            _articleBusiness = articleBusiness;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            IAsyncEnumerable<Article> articles = _articleBusiness.GetAllArticles();
            ICollection<ArticleViewModel> articlesViewModels = new List<ArticleViewModel>();

            await foreach (Article article in articles)
            {
                articlesViewModels.Add(_mapper.Map<ArticleViewModel>(article));
            }

            return View(articlesViewModels);
        }

        public async Task<IActionResult> View(int id)
        {
            Article? article = await _articleBusiness.GetArticleByIdAsync(id);

            if(article is not null)
            {
                return View(_mapper.Map<ArticleViewModel>(article));
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
        public async Task<IActionResult> Create(ArticleCreateViewModel article)
        {
            if(ModelState.IsValid)
            {
                Article createdArticle = _mapper.Map<Article>(article);
                try
                {
                    await _articleBusiness.CreateArticleAsync(createdArticle);
                }
                catch(DataValidationException ex)
                {
                    ModelState.AddModelError(ex.FieldName, ex.Message);

                    return View(article);
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex.Message);

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
            Article? article = await _articleBusiness.GetArticleByIdAsync(id);

            if (article is not null)
            {
                return View(_mapper.Map<ArticleEditViewModel>(article));
            }
            else
            {
                TempData["error"] = "The article could not be found.";

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ArticleEditViewModel article)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _articleBusiness.UpdateArticleAsync(_mapper.Map<Article>(article));
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
                    _logger.LogError(ex.Message);

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

        public async Task<IActionResult> CheckTopicUnicity(string topic, int id = default)
        {
            return Json(await _articleBusiness.CheckArticleTopicUnicity(topic, id));
        }

        public async Task<IActionResult> Delete(int id)
        {
            Article? article = await _articleBusiness.GetArticleByIdAsync(id);

            if (article is null)
            {
                TempData["error"] = "The article could not be found.";
            }
            else
            {
                await _articleBusiness.DeleteArticleAsync(article);

                TempData["success"] = "The article has been successfully deleted.";
            }

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteJson(int id)
        {
            Article? article = await _articleBusiness.GetArticleByIdAsync(id);

            if (article is null)
            {
                return NotFound();
            }
            else
            {
                await _articleBusiness.DeleteArticleAsync(article);

                return Ok();
            }
        }

        public async Task<IActionResult> SearchAjax(string? topic, string? content, string? author)
        {
            ICollection<ArticleViewModel> results = new List<ArticleViewModel>();
            await foreach(Article article in _articleBusiness.FindArticle(topic, content, author))
            {
                results.Add(_mapper.Map<ArticleViewModel>(article));
            }
            
            return PartialView("_ArticlesList", results);
        }
    }
}
