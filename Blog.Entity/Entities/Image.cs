using Blog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.Entities
{
    public class Image : EntityBase
    {
        private readonly string filename;
        private readonly string filetype;

        public Image()
        {
        }
        public Image(string filename,string filetype)
        {
            this.filename = filename;
            this.filetype = filetype;
        }



        // public Guid Id { get; set; } hepsini miras alıyoruz tüm sınıflarda ortak id alanı olduğu için
        public string FileName { get; set; }

        public string FileType { get; set; }

        public ICollection<Article> Articles { get; set; }

        public ICollection<AppUser> Users { get; set; }
    
    
    }
}
