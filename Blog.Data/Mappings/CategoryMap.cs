using Blog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Mappings
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(new Category
            {
                Id = Guid.Parse("D2B8F597-96F8-418B-9819-96C150CAD909"),
                Name = "asp .net core mvc",
                CreatedBy = "Admin test",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                DeletedBy="false",
               ModifiedBy="admin",
               
            },
            new Category
            {
                Id = Guid.Parse("3C5C732F-92E5-4707-B26E-E3482A60540F"),
                Name = "vs 2022",
                CreatedBy = "Admin test",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                DeletedBy = "false",
                ModifiedBy = "admin",
            }
            );
        }
    }
}
