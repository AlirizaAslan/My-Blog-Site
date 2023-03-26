using Blog.Core.Entities;
using Blog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.Entities
{ //classın ismi sql tablosundaki tablonun ismi ile aynı olmalı
    public class Article : EntityBase
    {
        private readonly Guid imageId;

        public Article()
        {
        }

        public Article(string title,string content,Guid UserId,string creatdBy, Guid categoryId,Guid imageId)
        {
            Title = title;
            Content = content;
            this.UserId = UserId;
            CategoryId = categoryId;
            this.imageId = imageId;
            CreatedBy = creatdBy;
           


        }


        public string Title { get; set; }

        public string Content { get; set; }

        public int ViewCount { get; set; } = 0;


        public Guid CategoryId { get; set; }

        public Category Category { get; set; } 
        

        public Guid? ImageId { get; set; }

        public Image Image { get; set; }

       
        public Guid UserId { get; set; }
        public AppUser User { get; set; }   

             

    }
}