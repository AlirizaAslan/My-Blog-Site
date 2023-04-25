using Blog.Entity.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.FluentValidations
{
    public class CategoryValidator: AbstractValidator<Category> //assembly ile kendisi otomatik buluyor tekrar tanımlamamaıza gerek yok  https://www.youtube.com/watch?v=gurfH5qNswY&list=PLrSCwxkucNmxFrrAsGm14Z-5Cu52MKrNr&index=25 dakka 13.30

    {

        public CategoryValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(100)
                .WithName("Kategori Adı");
        }  
    }
}
