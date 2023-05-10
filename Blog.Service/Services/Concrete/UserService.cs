using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entity.Entities;
using Blog.Entity.Enum;
using Blog.Entity.Helpers.images;
using Blog.Entity.VİewModels.Users;
using Blog.Service.Extensions;
using Blog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IImageHelper imageHelper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly ClaimsPrincipal _user;

        public UserService(IUnitOfWork unitOfWork,IImageHelper imageHelper,IHttpContextAccessor httpContextAccessor,IMapper mapper,UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,RoleManager<AppRole> roleManager) 
        {
            this.unitOfWork = unitOfWork;
            this.imageHelper = imageHelper;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            _user = httpContextAccessor.HttpContext.User;
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

        public async Task<UserProfileDto> GetUserProfileAsync()
        {
            var userId= _user.GetLoggerInUserId();

            var getUserWithImage = await unitOfWork.GetRepository<AppUser>().GetAsync(x => x.Id == userId, i => i.Image);
            var map = mapper.Map<UserProfileDto>(getUserWithImage);
            map.Image.FileName = getUserWithImage.Image.FileName;

            return map;

        }

        private async Task<Guid> UploadImageForUser (UserProfileDto userProfileDto)
        {  //ortak bir yere taşıdık iki yerde de kullanmak içisn
            var userEmail = _user.GetLoggerInUserEmail();

            var imageUpload = await imageHelper.Upload($"{userProfileDto.FirstName}{userProfileDto.LastName}", userProfileDto.Photo, ImageType.Post);
            Image image = new(imageUpload.FullName, userProfileDto.Photo.ContentType, userEmail);
            await unitOfWork.GetRepository<Image>().AddAsync(image);

            return image.Id;

        }


        public async Task<bool> UserProfileUpdateAsync(UserProfileDto userProfileDto)
        {
            var userId=_user.GetLoggerInUserId();
            var user=await GetAppUserByIdAsync(userId);

            var isVerified = await userManager.CheckPasswordAsync(user, userProfileDto.CurrentPassword);  //şşifre kontrolü yapıyoruz
            if (isVerified && userProfileDto.NewPassword != null )
            {
                var result = await userManager.ChangePasswordAsync(user, userProfileDto.CurrentPassword, userProfileDto.NewPassword);

                if (result.Succeeded)
                {  //bu işlemler için service yazmak daha clean bir yöntem
                    await userManager.UpdateSecurityStampAsync(user);
                    await signInManager.SignOutAsync();
                    await signInManager.PasswordSignInAsync(user, userProfileDto.NewPassword, true, false);  //geri kendimiz giriş yaptırdık

                    mapper.Map(userProfileDto, user);

                    //user.FirstName = userProfileDto.FirstName;
                    //user.LastName = userProfileDto.LastName;
                    //user.PhoneNumber = userProfileDto.PhoneNumber;

                    if(userProfileDto.Photo != null)
                    {
                        user.ImageId = await UploadImageForUser(userProfileDto);

                    }



                    await userManager.UpdateAsync(user);
                    await unitOfWork.SaveAsync();

                    return true;
                  
                }
                else
                {
                    return false;
                }
            }
            else if (isVerified)
            {
                await userManager.UpdateSecurityStampAsync(user);
                mapper.Map(userProfileDto, user);


                if (userProfileDto.Photo != null)
                {
                    user.ImageId = await UploadImageForUser(userProfileDto);

                }


                await userManager.UpdateAsync(user);        
                await unitOfWork.SaveAsync();
                
                return true;
            }

         

            else
            {

                return false;
            }


        }
    }
}
