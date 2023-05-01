using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entity.Entities;
using Blog.Entity.VİewModels.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Services.Abstractions
{
    public interface IArticleService
    {
        Task<List<ArticleDtos>> GetAllArticlesWithCategoryNonDeleteAsync();
        Task<List<ArticleDtos>> GetAllArticlesWithCategoryDeletedAsync();

        Task<ArticleDtos> GetArticlesWithCategoryNonDeleteAsync(Guid articleId);



        Task<string> UpdateArticleAsync(ArticleUpdateDto articleUpdateDto);
        Task<string> SafeDeleteArticleAsync(Guid articleId);
        Task<string> UndoDeleteArticleAsync(Guid articleId);


        Task CreateArticleAsync(ArticleAddDto articleAddDto);
    }
}
