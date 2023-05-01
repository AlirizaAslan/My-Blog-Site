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
        private readonly UserManager<AppUser> userManager;
        private readonly IUserService userService;
        private readonly IUnitOfWork unitOfWork;
        private readonly RoleManager<AppRole> roleManager;
        private readonly IImageHelper imageHelper;
        private readonly IValidator<AppUser> validator;
        private readonly IToastNotification toastNotification;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IMapper mapper;

        public UserController(UserManager<AppUser> userManager,IUserService userService,IUnitOfWork unitOfWork,RoleManager<AppRole> roleManager,IImageHelper imageHelper,IValidator<AppUser> validator,IToastNotification toastNotification,SignInManager<AppUser> signInManager,IMapper mapper)
        {
            this.userManager = userManager;
            this.userService = userService;
            this.unitOfWork = unitOfWork;
            this.roleManager = roleManager;
            this.imageHelper = imageHelper;
            this.validator = validator;
            this.toastNotification = toastNotification;
            this.signInManager = signInManager;
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
                //    ModelState.AddModelError("", item.Description);
                result.identityResult.AddToIdentityModelState(this.ModelState);

            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user=await userManager.GetUserAsync(HttpContext.User); //bağlı olan kullanıcı alıyoruz
            var getImage=await unitOfWork.GetRepository<AppUser>().GetAsync(x=>x.Id==user.Id,i=>i.Image);
            var map=mapper.Map<UserProfileDto>(user);
            map.Image.FileName=getImage.Image.FileName;
            return View(map);
        }
        [HttpPost]
        public async Task<IActionResult> Profile(UserProfileDto userProfileDto)
        {
            var user = await userManager.GetUserAsync(HttpContext.User); //bağlı olan kullanıcı alıyoruz

            if (ModelState.IsValid) 
            {
                var isVerified = await userManager.CheckPasswordAsync(user, userProfileDto.CurrentPassword);  //şşifre kontrolü yapıyoruz
                if(isVerified && userProfileDto.NewPassword!=null && userProfileDto.Photo != null) 
                {
                    var result = await userManager.ChangePasswordAsync(user, userProfileDto.CurrentPassword, userProfileDto.NewPassword);

                    if (result.Succeeded) 
                    {  //bu işlemler için service yazmak daha clean bir yöntem
                        await userManager.UpdateSecurityStampAsync(user);
                        await signInManager.SignOutAsync();
                        await signInManager.PasswordSignInAsync(user, userProfileDto.NewPassword, true, false);  //geri kendimiz giriş yaptırdık


                        user.FirstName = userProfileDto.FirstName;
                        user.LastName = userProfileDto.LastName;
                        user.PhoneNumber = userProfileDto.PhoneNumber;

                        var imageUpload = await imageHelper.Upload($"{userProfileDto.FirstName}{userProfileDto.LastName}", userProfileDto.Photo, ImageType.Post);
                        Image image = new(imageUpload.FullName, userProfileDto.Photo.ContentType, user.Email);

                        await unitOfWork.GetRepository<Image>().AddAsync(image);

                        await userManager.UpdateAsync(user);

                        user.ImageId = image.Id;

                        await unitOfWork.SaveAsync();

                        toastNotification.AddSuccessToastMessage("şifreniz ve bilgileriniz başarıyla güncellenmiştir");

                        return View();
                    }
                    else
                    {
                        result.AddToIdentityModelState(ModelState); return View();
                    }

                   
                }
                else if (isVerified &&userProfileDto.Photo!=null)
                {
                    await userManager.UpdateSecurityStampAsync(user);
                    user.FirstName = userProfileDto.FirstName;
                    user.LastName = userProfileDto.LastName;
                    user.PhoneNumber = userProfileDto.PhoneNumber;
                    

                    var imageUpload = await imageHelper.Upload($"{userProfileDto.FirstName}{userProfileDto.LastName}", userProfileDto.Photo, ImageType.Post);
                    Image image = new(imageUpload.FullName, userProfileDto.Photo.ContentType, user.Email);

                    await unitOfWork.GetRepository<Image>().AddAsync(image);

                    await userManager.UpdateAsync(user);

                    user.ImageId = image.Id;

                    await unitOfWork.SaveAsync();

                    toastNotification.AddSuccessToastMessage("Bilgileriniz başarıyla güncellenmiştir");
                    return View();
                }

                else 
                {
                    toastNotification.AddSuccessToastMessage("Bilgileriniz güncellenirken bir hata oluştu");

                    return View();
                }


            }


            return View();
        }

    }
}
