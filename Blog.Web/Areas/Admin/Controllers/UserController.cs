using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entity.Entities;
using Blog.Entity.Enum;
using Blog.Entity.Helpers.images;
using Blog.Entity.VİewModels.Articles;
using Blog.Entity.VİewModels.Users;
using Blog.Service.Extensions;
using Blog.Service.Services.Abstractions;
using Blog.Web.ResultMessages;
using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.ComponentModel.DataAnnotations;
using System.Data;
using static Blog.Web.ResultMessages.Messages;

namespace Blog.Web.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IValidator<AppUser> validator;
        private readonly IToastNotification toastNotification;
        private readonly IMapper mapper;

        public UserController(IUserService userService,IValidator<AppUser> validator,IToastNotification toastNotification,IMapper mapper)
        {
            this.userService = userService;
            this.validator = validator;
            this.toastNotification = toastNotification;
            this.mapper = mapper;
        }

        public async Task< IActionResult> Index()
        {
          var result= await userService.GetAllUsersWithRoleAsync();
          return View(result);
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var roles=await userService.GetAllRolesAsync();
            return View(new UserAddDto { Roles=roles });
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserAddDto userAddDto)
        {
            var map = mapper.Map<AppUser> (userAddDto);
            var validation = await validator.ValidateAsync(map);
            var roles = await userService.GetAllRolesAsync();

            if (ModelState.IsValid) 
            {
                var result=await userService.CreateUserAsync(userAddDto);

                if (result.Succeeded) 
                {                    
                    toastNotification.AddSuccessToastMessage(Messages.User.Add(userAddDto.Email), new ToastrOptions() { Title = "işlem başarılı" });
                    return RedirectToAction("Index", "User", new { Area = "Admin" });
                }

                else
                {
                    //foreach (var item in result.Errors)  addtoıdentity ile hallettik ders 30 dakka 26                  
                    //    ModelState.AddModelError("", item.Description);
                    result.AddToIdentityModelState(this.ModelState);
                    validation.AddToModelState2(this.ModelState);
                    return View(new UserAddDto { Roles = roles });                    
                }

            }
                return View(new UserAddDto { Roles = roles });
            
        }

        [HttpGet]

        public async Task<IActionResult> Update(Guid userId)
        {
            var user= await userService.GetAppUserByIdAsync(userId);
            var roles = await userService.GetAllRolesAsync();
            var map=mapper.Map<UserUpdateDto>(user);
             map.Roles = roles;// map.Roles de rol yok
            return View(map);
        }

        [HttpPost]

        public async Task<IActionResult> Update(UserUpdateDto userUpdateDto)
        {
            var user = await userService.GetAppUserByIdAsync(userUpdateDto.Id);
            if (user != null) {
              var roles = await userService.GetAllRolesAsync();
                if (ModelState.IsValid) 
                {
                    var map = mapper.Map(userUpdateDto,user);
                    var validation = await validator.ValidateAsync(map);
                     if (validation.IsValid)
                    {                      
                    //user.FirstName = userUpdateDto.FirstName;
                    //user.LastName = userUpdateDto.LastName;
                    //user.Email = userUpdateDto.Email;
                    user.UserName = userUpdateDto.Email;
                    user.SecurityStamp=Guid.NewGuid().ToString();
                        var result = await userService.UpdateUserAsync(userUpdateDto);
                     if (result.Succeeded)
                        {                           
                            toastNotification.AddSuccessToastMessage(Messages.User.Add(userUpdateDto.Email), new ToastrOptions() { Title = "işlem başarılı" });
                            return RedirectToAction("Index", "User", new { Area = "Admin" });
                        }
                        else
                        {
                            //foreach (var item in result.Errors)
                            //    ModelState.AddModelError("", item.Description);
                            result.AddToIdentityModelState(this.ModelState);
                            validation.AddToModelState2(this.ModelState);

                            return View(new UserUpdateDto { Roles = roles });
                        }
                    }

                    else 
                    {
                        validation.AddToModelState2(this.ModelState);
                        return View(new UserUpdateDto { Roles = roles });
                    }       
                }                
            }
            return NotFound();
        }


        public async Task<IActionResult> Delete(Guid userId)
        {
            var result = await userService.DeleteUserAsync(userId);
            if (result.identityResult.Succeeded)
            {                
                toastNotification.AddSuccessToastMessage(Messages.User.Delete(result.email), new ToastrOptions() { Title = "işlem başarılı" });
                return RedirectToAction("Index", "User", new { Area = "Admin" });
            }

            else
            {
                //foreach (var item in result.Errors)
                //ModelState.AddModelError("", item.Description);
                result.identityResult.AddToIdentityModelState(this.ModelState);

            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var profile = await userService.GetUserProfileAsync();
            return View(profile);
        }
        [HttpPost]
        public async Task<IActionResult> Profile(UserProfileDto userProfileDto)
        {
            
            if (ModelState.IsValid) 
            {
                 var result=await userService.UserProfileUpdateAsync(userProfileDto);
                if (result)
                {
                    toastNotification.AddSuccessToastMessage("ProfilGüncelleme İşlemi tamamlandı.", new ToastrOptions() { Title = "işlem başarılı" });
                    return RedirectToAction("Index", "User", new { Area = "Admin" });

                }
                else
                {
                    var profile=await userService.GetUserProfileAsync();
                    toastNotification.AddErrorToastMessage("ProfilGüncelleme İşlemi tamamlanamadı", new ToastrOptions() { Title = "işlem başarılı" });
                    return View(profile);
                }

            }

            else
            {
               return NotFound();
            }
                     
        }

    }
}
