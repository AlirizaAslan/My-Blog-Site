using Blog.Data.Context;
using Blog.Data.Repositories.Abstractions;
using Blog.Data.Repositories.Concretes;
using Blog.Service.FluentValidations;
using Blog.Service.Services.Abstractions;
using Blog.Service.Services.Concrete;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Extensions
{
    public static class ServiceLayerExtension
    {
        public static IServiceCollection LoadServiceLayerExtension(this IServiceCollection services)
        {
            var assembly= Assembly.GetExecutingAssembly();
            services.AddAutoMapper(assembly);

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IArticleService, ArticleService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); //Ihttp  http tipine dönüştürülüyor

            services.AddControllersWithViews().AddFluentValidation(opt =>
            {
                opt.RegisterValidatorsFromAssemblyContaining<ArticleValidator>();
                opt.DisableDataAnnotationsValidation=true;

                opt.ValidatorOptions.LanguageManager.Culture = new System.Globalization.CultureInfo("tr");  //https://www.youtube.com/watch?v=bux0VUhpyBo&list=PLrSCwxkucNmxFrrAsGm14Z-5Cu52MKrNr&index=20  dakka 15


            });
             return services;
        }
    }
}
