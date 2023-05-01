using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entity.Entities;
using Blog.Entity.VİewModels.Users;
using Blog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;

        public UserService(IUnitOfWork unitOfWork,IMapper mapper,UserManager<AppUser> userManager,RoleManager<AppRole> roleManager) 
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IdentityResult> CreateUserAsync(UserAddDto userAddDto)
        {
            var map = mapper.Map<AppUser> (userAddDto);
            map.UserName = userAddDto.Email;
            var result = await userManager.CreateAsync(map, string.IsNullOrEmpty(userAddDto.Password) ? "" : userAddDto.Password);

            if(result.Succeeded) 
            {
                var findRole = await roleManager.FindByIdAsync(userAddDto.RoleId.ToString());
                await userManager.AddToRoleAsync(map, findRole.ToString());
                return result;
            }
            else
            {
                return result;
            }
        }

        public async Task<(IdentityResult identityResult, string? email)> DeleteUserAsync(Guid userId)
        {
            //2 tane result(değer) döndük
            var user = await GetAppUserByIdAsync(userId);
            var result = await userManager.DeleteAsync(user);

            if(result.Succeeded) 
            {
                return (result,user.Email);

            }
            else { return (result,null); }
        }

        public async Task<List<AppRole>> GetAllRolesAsync()
        {

            return await roleManager.Roles.ToListAsync();

        }

        public async Task<List<UserDto>> GetAllUsersWithRoleAsync()
        {
            var users = await userManager.Users.ToListAsync();
            var map = mapper.Map<List<UserDto>>(users);


            foreach (var item in map)
            {
                var findUser = await userManager.FindByIdAsync(item.Id.ToString());
                var role = string.Join("", await userManager.GetRolesAsync(findUser)); //kullanıcın bir  tane rolünü için join https://www.youtube.com/watch?v=RXczVzk_pWE&list=PLrSCwxkucNmxFrrAsGm14Z-5Cu52MKrNr&index=27   dk 19
                //user =superadmin@gmail.com
                //superadmin
                //admin
                //user  gibi 3 rolü varsa  join ile "superadmin-admin-user" diye ayırarak yazılır


                item.Role = role; //bir kullanıcının bir rolü olarak düşünüyoruz 

            }

            return map;
           
        }

        public async Task<AppUser> GetAppUserByIdAsync(Guid userId)
        {

            return await userManager.FindByIdAsync(userId.ToString());

        }

        public async Task<string> GetUserRoleAsync(AppUser user)
        {
            return string.Join("", await userManager.GetRolesAsync(user));
        }

        public async Task<IdentityResult> UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
            var user = await GetAppUserByIdAsync(userUpdateDto.Id);
            var userRole = await GetUserRoleAsync(user);

            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                await userManager.RemoveFromRoleAsync(user, userRole);  //rolü sildik

                var findRole = await roleManager.FindByIdAsync(userUpdateDto.RoleId.ToString());
                await userManager.AddToRoleAsync(user, findRole.Name.ToString());
                return result;
            }
            else
            return result;
        }
    }
}
