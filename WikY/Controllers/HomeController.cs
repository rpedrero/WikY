using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WikY.Business.Contracts;
using WikY.Entities;
using WikY.Models;

namespace WikY.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArticleBusiness _articleBusiness;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IArticleBusiness articleBusiness, IMapper mapper)
        {
            _logger = logger;
            _articleBusiness = articleBusiness;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            Article? lastArticle = await _articleBusiness.GetLastArticle();
            
            return View(_mapper.Map<ArticleViewModel>(lastArticle));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
