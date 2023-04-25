using AutoMapper;
using Blog.Entity.Entities;
using Blog.Entity.VİewModels.Categories;
using Blog.Service.Extensions;
using Blog.Service.Services.Abstractions;
using Blog.Service.Services.Concrete;
using Blog.Web.ResultMessages;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IValidator<Category> validator;
        private readonly IMapper mapper;
        private readonly IToastNotification toast;

        

        public CategoryController(ICategoryService categoryService,IValidator<Category> validator,IMapper mapper,IToastNotification toast) 
        {
            this.categoryService = categoryService;
            this.validator = validator;
            this.mapper = mapper;
            this.toast = toast;
        }

        public async Task <IActionResult> Index()
        {
            var categories=await categoryService.GetAllCategoriesNonDeleted();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task< IActionResult> Add(CategoryAddDto categoryAddDto)
        { 
            var map=mapper.Map<Category>(categoryAddDto); 
            var result =await validator.ValidateAsync(map);


            if(result.IsValid) 
            {
               await categoryService.CreateCategoryAsync(categoryAddDto);
                toast.AddSuccessToastMessage(Messages.Category.Add(categoryAddDto.Name), new ToastrOptions{ Title = "işlem başarılı" });

                return RedirectToAction("Index", "Category", new {Area="Admin"});
            }
                result.AddToModelState2(this.ModelState); return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddWithAjax([FromBody] CategoryAddDto categoryAddDto)
        {
            var map = mapper.Map<Category>(categoryAddDto);
            var result = await validator.ValidateAsync(map);

            if (result.IsValid)
            {
                await categoryService.CreateCategoryAsync(categoryAddDto);
                toast.AddSuccessToastMessage(Messages.Category.Add(categoryAddDto.Name), new ToastrOptions { Title = "işlem başarılı" });

                return Json(Messages.Category.Add(categoryAddDto.Name));
            }

            else 
            {
                toast.AddErrorToastMessage(result.Errors.First().ErrorMessage, new ToastrOptions { Title = "işlem başarısız" });

                return Json(result.Errors.First().ErrorMessage);

            }

            


        }


        [HttpGet]
        public async Task <IActionResult> Update(Guid categoryId)
        {
            var category=await categoryService.GetCategoryByGuid(categoryId);

            var map=mapper.Map<Category,CategoryUpdateDto>(category);

           // return View(new CategoryUpdateDto() { Id=category.Id ,Name=category.Name});
            return View(map);

        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateDto categoryUpdateDto)
        {
            var map = mapper.Map<Category>(categoryUpdateDto);
            var result = await validator.ValidateAsync(map);


            if( result.IsValid) 
            {
                var name=await categoryService.UpdateCategoryAsync(categoryUpdateDto);
                toast.AddSuccessToastMessage(Messages.Category.Update(name), new ToastrOptions { Title = "İşlem başarılı" });

                return RedirectToAction("Index", "Category", new { Area = "Admin" });
            }

            result.AddToModelState2(this.ModelState);
            return View();

        }




        public async Task<IActionResult> Delete(Guid categoryId)
        {
            var name = await categoryService.SafeDeleteArticleAsync(categoryId);

            toast.AddSuccessToastMessage(Messages.Category.Delete(name), new ToastrOptions() { Title = "işlem başarılı" });

            return RedirectToAction("Index", "Category", new { Area = "Admin" });

        }

    }
}
