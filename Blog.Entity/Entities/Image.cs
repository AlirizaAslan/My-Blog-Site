using Blog.Core.Entities;
using Blog.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.Entities
{
    public class Image : EntityBase
    {
      

		public Image()
        {
        }


        public Image(string filename,string filetype,string createdBy)
        {
           FileName = filename;
            FileType = filetype;
            CreatedBy = createdBy;
            
		}



        // public Guid Id { get; set; } hepsini miras alıyoruz tüm sınıflarda ortak id alanı olduğu için
        public string FileName { get; set; }

        public string FileType { get; set; }

        

        public ICollection<Article> Articles { get; set; }

        public ICollection<AppUser> Users { get; set; }
    
    
    }
}
