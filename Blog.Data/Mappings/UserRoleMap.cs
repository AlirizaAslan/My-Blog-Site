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
    public class UserRoleMap : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            // Primary key
            builder.HasKey(r => new { r.UserId, r.RoleId });

            // Maps to the AspNetUserRoles table
            builder.ToTable("AspNetUserRoles");

            builder.HasData(new AppUserRole
            {
                UserId= Guid.Parse("9EE7F3A2-9787-4F52-AF19-11DD788BA40F"),
                RoleId = Guid.Parse("B90BC202-5666-4016-9A46-2F6D5E9BFF6E"),
            },
            new AppUserRole
            {
                UserId= Guid.Parse("52296E36-52D2-4A53-858C-C884BAA6E4CC"),
                RoleId = Guid.Parse("326B8367-9F58-432F-8929-495FE67C698B"),
            }
            
            
            );




        }
    }
}
