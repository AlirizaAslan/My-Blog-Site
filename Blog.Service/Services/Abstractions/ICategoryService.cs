using Blog.Entity.Entities;
using Blog.Entity.VİewModels.Categories;
using Blog.Service.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Services.Abstractions
{
    public interface  ICategoryService
    {
        Task<List<CategoryDto>> GetAllCategoriesNonDeleted();
        Task<List<CategoryDto>> GetAllCategoriesDeleted();
        Task CreateCategoryAsync(CategoryAddDto categoryAddDto);
        Task<Category> GetCategoryByGuid(Guid Id);
        Task<string> UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto);
        Task<string> SafeDeleteArticleAsync(Guid categoryId);
        Task<string> UndoDeleteArticleAsync(Guid categoryId);
    }
}
