using Blog.Core.Entities;
using Blog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;



namespace Blog.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        
        public DbSet<Article> articles { get; set; }

        public DbSet<Category> categories { get; set; }

        public DbSet<Image> ımages { get; set; }

        //model oluşmadan konfigürasyon yapmamızı sağlayacak olan sınıf
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {//her tablo için tek yerden yapmamızı sağlıyor bu kod
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
