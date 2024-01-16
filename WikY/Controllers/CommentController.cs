using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WikY.Business.Contracts;
using WikY.Business.Exceptions;
using WikY.Entities;
using WikY.Models;

namespace WikY.Controllers
{
    public class CommentController : Controller
    {
        private ILogger<Comment> _logger;
        private IArticleBusiness _articleBusiness;
        private ICommentBusiness _commentBusiness;
        private IMapper _mapper;

        public CommentController(ILogger<Comment> logger, IArticleBusiness articleBusiness, ICommentBusiness commentBusiness, IMapper mapper)
        {
            _logger = logger;
            _articleBusiness = articleBusiness;
            _commentBusiness = commentBusiness;
            _mapper = mapper;
        }
        
        public async Task<IActionResult> CreateForArticle(int id)
        {
            Article? article = await _articleBusiness.GetArticleById(id);
            if (article is not null)
            {
                CommentViewModel comment = new CommentViewModel { ArticleId = article.Id };

                return View(comment);
            }
            else
            {
                TempData["error"] = "Article not found.";

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CommentViewModel comment)
        {
            if (ModelState.IsValid)
            {
                Comment commentToCreate = _mapper.Map<Comment>(comment);

                Article? article = await _articleBusiness.GetArticleById(comment.ArticleId);
                if (article is not null)
                {
                    commentToCreate.Article = article;

                    try
                    {
                        await _commentBusiness.CreateComment(commentToCreate);
                    }
                    catch (DataValidationException ex)
                    {
                        ModelState.AddModelError(ex.FieldName, ex.Message);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);

                        TempData["error"] = "An unexpected error has occurred when attempting to create the comment. Try again later.";

                        return RedirectToAction("CreateForArticle", comment);
                    }

                    return RedirectToAction("View", "Article", new { article.Id });
                }
                else
                {
                    TempData["error"] = "Specified article does not exist.";

                    return RedirectToAction("Index", "Article");
                }
            }
            else
            {
                return RedirectToAction("CreateForArticle", comment);
            }
        }
    }
}
