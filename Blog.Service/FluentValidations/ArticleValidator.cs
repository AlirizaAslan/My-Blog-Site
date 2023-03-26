using Blog.Entity.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.FluentValidations
{
    public class ArticleValidator: AbstractValidator<Article> //aslında articleAdd ve ArticeUpdate için validations işlemi lazım biz map ile dönüştürme yapacz  https://www.youtube.com/watch?v=bux0VUhpyBo&list=PLrSCwxkucNmxFrrAsGm14Z-5Cu52MKrNr&index=20 dakka 6
    {
        public ArticleValidator() 
        {
            RuleFor(x => x.Title).NotEmpty().NotNull().MinimumLength(3).MaximumLength(150).WithName("başlık");

            RuleFor(x=>x.Content).NotEmpty().NotNull().MinimumLength(3).MaximumLength(150).WithName("kategori");

        }

            

    }
}
