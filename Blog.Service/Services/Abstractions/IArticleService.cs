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

    }
}
