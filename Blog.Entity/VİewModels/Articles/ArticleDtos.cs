using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.VİewModels.Articles
{
    public class ArticleDtos
    {  //dto nesnesi oluşturduk
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
        public int ViewCount { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }
    }
}
