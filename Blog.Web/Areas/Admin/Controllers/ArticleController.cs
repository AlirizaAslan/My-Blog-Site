using AutoMapper;
using Blog.Entity.Entities;
using Blog.Entity.VİewModels.Articles;
using Blog.Service.Extensions;
using Blog.Service.Services.Abstractions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Blog.Service.Extensions;
using NToastNotify;
using Blog.Web.ResultMessages;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;
        private readonly IValidator<Article> validator;
        private readonly IToastNotification toastNotification;

        public ArticleController(IArticleService articleService,ICategoryService categoryService,IMapper mapper,IValidator<Article> validator,IToastNotification toastNotification)
        {
            this.articleService = articleService;
            this.categoryService = categoryService;
            this.mapper = mapper;
            this.validator = validator;
            this.toastNotification = toastNotification;
        }

        public async Task< IActionResult> Index()
        {

            var articles=await articleService.GetAllArticlesWithCategoryNonDeleteAsync();
            return View(articles);
        }

        [HttpGet]
        public async Task<IActionResult> Add() 
        {
            var categories = await categoryService.GetAllCategoriesNonDeleted();
            return View(new ArticleAddDto { Categories=categories});
        }

        [HttpPost]
        public async Task<IActionResult> Add(ArticleAddDto articleAddDto)
        {
            var map=mapper.Map<Article>(articleAddDto);   
            var result = await validator.ValidateAsync(map); // dakka 19 https://www.youtube.com/watch?v=bux0VUhpyBo&list=PLrSCwxkucNmxFrrAsGm14Z-5Cu52MKrNr&index=20

            if (result.IsValid)
            {
                await articleService.CreateArticleAsync(articleAddDto);

                toastNotification.AddSuccessToastMessage(Messages.Article.Add(articleAddDto.Title),new ToastrOptions { Title="işlem Başarılı"});

                return RedirectToAction("Index", "Article", new { Area = "Admin" });
            }
            
            else
            {
                result.AddToModelState2(this.ModelState);
               
                
            }

            var categories = await categoryService.GetAllCategoriesNonDeleted();

            return View(new ArticleAddDto { Categories = categories });
        }


        [HttpGet]
        public async Task<IActionResult> Update(Guid articleId )
        { //her makalenin bir tane kategorisi olduğu için tek bir değer dönürmemeiz lazım 


            //https://www.youtube.com/watch?v=pWjNMhoYglw&list=PLrSCwxkucNmxFrrAsGm14Z-5Cu52MKrNr&index=17  dakka 22  
            var article = await articleService.GetArticlesWithCategoryNonDeleteAsync(articleId);

            var categories=await categoryService.GetAllCategoriesNonDeleted();

            var articleUpdateDto= mapper.Map<ArticleUpdateDto>(article);

            articleUpdateDto.Categories = categories;

            return View(articleUpdateDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ArticleUpdateDto articleUpdateDto)
        {


            var map =mapper.Map<Article>(articleUpdateDto);  
            var result=await validator.ValidateAsync(map);

            if (result.IsValid)
            {
               var title= await articleService.UpdateArticleAsync(articleUpdateDto);
                toastNotification.AddSuccessToastMessage(Messages.Article.Update(title),new ToastrOptions { Title = "işlem başarılı"});

                return RedirectToAction("Index", "Article", new { Area = "Admin" });
            }
            
            else
            {
                result.AddToModelState2(this.ModelState);
            }

            //https://www.youtube.com/watch?v=pWjNMhoYglw&list=PLrSCwxkucNmxFrrAsGm14Z-5Cu52MKrNr&index=17  dakka 30

            //await articleService.UpdateArticleAsync(articleUpdateDto);

            //var article = await articleService.GetArticlesWithCategoryNonDeleteAsync(articleId);

            var categories = await categoryService.GetAllCategoriesNonDeleted();

            articleUpdateDto.Categories = categories;

            return View(articleUpdateDto);
        }


        public async Task<IActionResult> Delete(Guid articleId)
        {
           var title= await articleService.SafeDeleteArticleAsync(articleId);

            toastNotification.AddSuccessToastMessage(Messages.Article.Delete(title), new ToastrOptions() { Title = "işlem başarılı" });

            return RedirectToAction("Index", "Article", new { Area = "Admin" });

        }

    }
}
