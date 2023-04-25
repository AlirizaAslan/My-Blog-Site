using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entity.Entities;
using Blog.Entity.VİewModels.Articles;
using Blog.Entity.VİewModels.Categories;
using Blog.Service.Extensions;
using Blog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ClaimsPrincipal _user;


        public CategoryService(IUnitOfWork unitOfWork,IMapper mapper,IHttpContextAccessor httpContextAccessor) 
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            _user = httpContextAccessor.HttpContext.User;
        }
        public async Task<List<CategoryDto>> GetAllCategoriesNonDeleted()
        {
            


          var categories=await unitOfWork.GetRepository<Category>().GetAllAsycn(x=>!x.IsDeleted);
          var map= mapper.Map<List<CategoryDto>>(categories);    

            return map;

        }

        public async Task CreateCategoryAsync(CategoryAddDto categoryAddDto)
        {
            var userId = _user.GetLoggerInUserId();
            var userEmail = _user.GetLoggerInUserEmail();
            Category category = new(categoryAddDto.Name, userEmail);
            await unitOfWork.GetRepository<Category>().AddAsync(category);
            await unitOfWork.SaveAsync();

        }



        public async Task<Category> GetCategoryByGuid(Guid Id)
        {
            var category = await unitOfWork.GetRepository<Category>().GetByGuidAsync(Id);

            return category;
        }


        public async Task<string> UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto)
        {
            var category= await unitOfWork.GetRepository<Category>().GetAsync(x => !x.IsDeleted && x.Id == categoryUpdateDto.Id);

            var useremail = _user.GetLoggerInUserEmail();

            category.Name = categoryUpdateDto.Name;
            category.ModifiedBy=useremail;
            category.ModifiedDate=DateTime.Now;

            await unitOfWork.GetRepository<Category>().UpdateAsync(category);
            await unitOfWork.SaveAsync();


            return category.Name;
        }

        public async Task<string> SafeDeleteArticleAsync(Guid categoryId)
        {
            var userEmail=_user.GetLoggerInUserEmail();
            var category=await unitOfWork.GetRepository<Category>().GetByGuidAsync(categoryId);


            category.DeletedBy=userEmail;   
            category.ModifiedDate=DateTime.Now; 
            category.IsDeleted=true;


            await unitOfWork.GetRepository<Category>().UpdateAsync(category);
            await unitOfWork.SaveAsync();

            return category.Name;


        }



    }
}
