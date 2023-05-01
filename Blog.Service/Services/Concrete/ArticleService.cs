using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entity.Entities;
using Blog.Entity.Enum;
using Blog.Entity.Helpers.images;
using Blog.Entity.VİewModels.Articles;
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
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
		private readonly IImageHelper imageHelper;
		private readonly ClaimsPrincipal _user;

        public ArticleService(IUnitOfWork unitOfWork,IMapper mapper,IHttpContextAccessor httpContextAccessor,IImageHelper imageHelper) 
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
			this.imageHelper = imageHelper;
			_user = httpContextAccessor.HttpContext.User;
        }

        public async Task CreateArticleAsync(ArticleAddDto articleAddDto)
        {
            // var userId = Guid.Parse("9EE7F3A2-9787-4F52-AF19-11DD788BA40F");

            var userId = _user.GetLoggerInUserId();
            var useremail=_user.GetLoggerInUserEmail();

            var imageUpload = await imageHelper.Upload(articleAddDto.Title,articleAddDto.Photo,ImageType.Post);

            Image image = new(imageUpload.FullName,articleAddDto.Photo.ContentType,useremail);
             
            await unitOfWork.GetRepository<Image>().AddAsync(image);

           
            var article = new Article(articleAddDto.Title, articleAddDto.Content, userId,useremail,articleAddDto.CategoryId, image.Id);
            //{
            //    Title = articleAddDto.Title,
            //    Content = articleAddDto.Content,
            //    CategoryId = articleAddDto.CategoryId,
            //    UserId = userId,
            //    CreatedBy="admin",
            //    DeletedBy="admin",
            //    ModifiedBy="admin",
                
                
            //};

            await unitOfWork.GetRepository<Article>().AddAsync(article);
            await unitOfWork.SaveAsync();
        }

        public async Task<List<ArticleDtos>> GetAllArticlesWithCategoryNonDeleteAsync()
        { //liste döndürüyor
            var articles= await unitOfWork.GetRepository<Article>().GetAllAsycn(x=>!x.IsDeleted,x=>x.Category);
            var map = mapper.Map<List< ArticleDtos>>(articles);
            return map;
        }

        public async Task<ArticleDtos> GetArticlesWithCategoryNonDeleteAsync(Guid articleId)
        { //tek bir değer döndürüyor
            var article = await unitOfWork.GetRepository<Article>().GetAsync(x => !x.IsDeleted && x.Id == articleId, x => x.Category,i=>i.Image) ;
            var map = mapper.Map<ArticleDtos>(article);
            return map;
        }

        public async Task<string> UpdateArticleAsync(ArticleUpdateDto articleUpdateDto)
        { //task<string> ile task string bir değer döndürecek
            var article =await unitOfWork.GetRepository<Article>().GetAsync(x=>!x.IsDeleted && x.Id==articleUpdateDto.Id, x => x.Category, i => i.Image);

            var useremail = _user.GetLoggerInUserEmail();

            if(articleUpdateDto.Photo != null)
            {
               imageHelper.Delete(article.Image.FileName);
               
                var imageUpload=await imageHelper.Upload(articleUpdateDto.Title,articleUpdateDto.Photo,ImageType.Post);
                Image image = new(imageUpload.FullName, articleUpdateDto.Photo.ContentType, useremail);
                await unitOfWork.GetRepository<Image>().AddAsync(image);
                //güncelleme yaparken resmi siliyoruz gereksiz yer kaplamasın diye

                article.ImageId = image.Id;


            }

            //article.Title = articleUpdateDto.Title;
            //article.Content = articleUpdateDto.Content;
            //article.CategoryId = articleUpdateDto.CategoryId;
            //article.ModifiedBy=useremail;
            article.ModifiedDate = DateTime.Now;
           mapper.Map(articleUpdateDto,article); //mapper ile tek satırla yapabilirsin 

            await unitOfWork.GetRepository<Article>().UpdateAsync(article);

            await unitOfWork.SaveAsync();

            return article.Title;
        }

        public async Task<string> SafeDeleteArticleAsync(Guid articleId) 
        {
            var article=await unitOfWork.GetRepository<Article>().GetByGuidAsync(articleId);
            var useremail = _user.GetLoggerInUserEmail();



            article.IsDeleted = true;
            article.DeletedDate = DateTime.Now;
            article.DeletedBy=useremail;

            await unitOfWork.GetRepository<Article>().UpdateAsync(article);


            await unitOfWork.SaveAsync();


            return article.Title;

        }

        public async Task<List<ArticleDtos>> GetAllArticlesWithCategoryDeletedAsync()
        {
            var articles = await unitOfWork.GetRepository<Article>().GetAllAsycn(x => x.IsDeleted, x => x.Category);
            var map = mapper.Map<List<ArticleDtos>>(articles);
            return map;
        }

        public async Task<string> UndoDeleteArticleAsync(Guid articleId)
        {
            var article = await unitOfWork.GetRepository<Article>().GetByGuidAsync(articleId);
            var useremail = _user.GetLoggerInUserEmail();



            article.IsDeleted = false;
            article.DeletedDate = null;
            article.DeletedBy = null;

            await unitOfWork.GetRepository<Article>().UpdateAsync(article);


            await unitOfWork.SaveAsync();


            return article.Title;
        }
    }
}
