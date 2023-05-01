using AutoMapper;
using Blog.Entity.Entities;
using Blog.Entity.VİewModels.Categories;
using Blog.Entity.VİewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.AutoMapper.Users
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<AppUser,UserDto>().ReverseMap();
            CreateMap<AppUser,UserAddDto>().ReverseMap();
            CreateMap<AppUser,UserUpdateDto>().ReverseMap();
            CreateMap<AppUser,UserProfileDto>().ReverseMap();
            
        }
    }
}
