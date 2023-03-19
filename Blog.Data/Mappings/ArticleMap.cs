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
                ViewCount = 1,
                CategoryId = Guid.Parse("D2B8F597-96F8-418B-9819-96C150CAD909"),                 
                Image= new Image
                {
                    Id = Guid.NewGuid(),

                     FileName="images/testimage",
                     FileType="jpg",
                     CreatedBy="Admin Test",
                     CreatedDate = DateTime.Now,
                     IsDeleted = false

                },
                ImageId=Guid.Parse("D5A78A10-21A8-49A3-A3B6-E21ADD292023"),
                CreatedBy="admin test",
                CreatedDate=DateTime.Now,
                IsDeleted=false,


            },
            new Article
            {

            }
            );
        
        
        }
    }
}
