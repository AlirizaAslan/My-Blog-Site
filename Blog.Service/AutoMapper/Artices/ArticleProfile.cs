using AutoMapper;
using Blog.Entity.Entities;
using Blog.Entity.VİewModels.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.AutoMapper.Artices
{
    public class ArticleProfile:Profile
    {
        public ArticleProfile()
        {
            CreateMap<ArticleDtos, Article>().ReverseMap();

            CreateMap<ArticleUpdateDto, Article>().ReverseMap();
            CreateMap<ArticleUpdateDto, ArticleDtos>().ReverseMap();
            CreateMap<ArticleAddDto, Article>().ReverseMap();



        }
    }
}
