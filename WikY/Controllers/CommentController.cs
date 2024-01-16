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
                return View(new CommentCreateViewModel { ArticleId = article.Id });
            }
            else
            {
                TempData["error"] = "Article not found.";

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateForArticle(CommentCreateViewModel comment)
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

                        return View(comment);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);

                        TempData["error"] = "An unexpected error has occurred when attempting to create the comment. Try again later.";

                        return View(comment);
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
                return View(comment);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAjax(CommentCreateViewModel comment)
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

                        return BadRequest(ModelState);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);

                        return Problem();
                    }

                    return PartialView("_Comment", _mapper.Map<CommentViewModel>(commentToCreate));
                }
                else
                {
                    return BadRequest("Article not found.");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            Comment? comment = await _commentBusiness.GetCommentById(id);
            if(comment is not null)
            {
                int articleId = comment.ArticleId;
                
                try
                {
                    await _commentBusiness.DeleteComment(comment);
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex.Message);
                    
                    TempData["error"] = "An unexpected error has occurred when attempting to delete the comment. Try again later.";

                    return RedirectToAction("View", "Article", new { Id = articleId });
                }
                
                TempData["success"] = "The comment has been successfully deleted.";

                return RedirectToAction("View", "Article", new { Id = articleId });
            }
            else
            {
                TempData["error"] = "Comment not found.";

                return RedirectToAction("Index", "Article");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAjax(int id)
        {
            Comment? comment = await _commentBusiness.GetCommentById(id);
            if (comment is not null)
            {
                try
                {
                    await _commentBusiness.DeleteComment(comment);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);

                    return Problem();
                }

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
