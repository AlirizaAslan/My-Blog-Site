using Blog.Entity.Entities;
using Blog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IArticleService articleService;
        

        public HomeController(IArticleService articleService,UserManager<AppUser> userManager)
        {
            this.articleService = articleService;
           
        }
        public async Task <IActionResult> Index()
        {
            var articles=await articleService.GetAllArticlesWithCategoryNonDeleteAsync();         


            return View(articles);
        }
    }
}
