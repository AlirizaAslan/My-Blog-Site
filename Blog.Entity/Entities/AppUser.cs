using Blog.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.Entities
{
    public class AppUser:IdentityUser<Guid>,IEntityBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid ImageId { get; set; } = Guid.Parse("80e25378-27d6-45d4-9ec0-ebe6ee25937b");
        public Image Image { get; set; }

        public ICollection<Article> Articles { get; set;}
    }
}
