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
    public class ArticleMap : IEntityTypeConfiguration<Article>
    {//burda tablodaki data type lerini belirlememizi sağlar örneği title kısmı 150 karaterle sınırladık
        public void Configure(EntityTypeBuilder<Article> builder)
        {
           builder.Property(x=> x.Title).HasMaxLength(150); //karakter sınırladık 150 oldu
            builder.Property(x => x.Title).IsRequired(false); //içerik girilmesi zorunlu değil

            builder.HasData(new Article
            {
                Id = Guid.NewGuid(),
                Title = "Asp.net core deneme makalesi 1",
                Content = "asp. net açıklama",
                ViewCount = 15,                
                CategoryId = Guid.Parse("D2B8F597-96F8-418B-9819-96C150CAD909"),                 
                
                ImageId=Guid.Parse("D5A78A10-21A8-49A3-A3B6-E21ADD292023"),
                CreatedBy="admin test",
                CreatedDate=DateTime.Now,
                IsDeleted=false,
                ModifiedBy="admin",
                DeletedBy="False",
                UserId= Guid.Parse("9EE7F3A2-9787-4F52-AF19-11DD788BA40F"),

            },
            new Article
            {
                Id = Guid.NewGuid(),
                Title = "vs deneme makalesi 1",
                Content = "vs açıklama",
                ViewCount = 1,                
                CategoryId = Guid.Parse("3C5C732F-92E5-4707-B26E-E3482A60540F"),              
                ImageId=Guid.Parse("9C6A7406-5073-45C5-B2DE-A56773C220B4"),
                CreatedBy = "admin test",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                DeletedBy = "admin",
                ModifiedBy = "False",
                UserId = Guid.Parse("52296E36-52D2-4A53-858C-C884BAA6E4CC"),
            }
            );
        
        
        }
    }
}
