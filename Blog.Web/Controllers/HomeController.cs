using Blog.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Blog.Service.Services.Abstractions;
using Blog.Service.Services.Concrete;
using Newtonsoft.Json;

namespace Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDashboardService dashboardService;
        private readonly IArticleService articleService;

        public HomeController(ILogger<HomeController> logger,IDashboardService dashboardService ,IArticleService articleService )
        {
            _logger = logger;
            this.dashboardService = dashboardService;
            this.articleService = articleService;
        }

        public async Task<IActionResult> Index()
        {
            var article = await articleService.GetAllArticlesWithCategoryNonDeleteAsync();
            return View(article);
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