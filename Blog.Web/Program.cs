 using Microsoft.EntityFrameworkCore;
using Blog.Data.Context;
using Blog.Data.Extensions;
using Blog.Service.Services;
using Blog.Service.Extensions;
using Blog.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using NToastNotify;
using Blog.Service.Describers;

var builder = WebApplication.CreateBuilder(args);

//program.cs kirletmeden extension ile �repsort �a��rd���m�zda repostroy de �a��r�yoruz
builder.Services.LoadDataLayerExtension(builder.Configuration);

builder.Services.LoadServiceLayerExtension();

builder.Services.AddSession();


// Add services to the container.
builder.Services.AddControllersWithViews().AddNToastNotifyToastr(new NToastNotify.ToastrOptions
{
    PositionClass=ToastPositions.TopRight, //sa� tarfata ��kacak
    TimeOut=3000  //3 saniye duracak
    
}).AddRazorRuntimeCompilation(); //razor ile viewde run time s�ras�nda de�i�ikleri g�rmemizi sa�l�yor


builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireLowercase = false;  //k���k harf zorunlulu�u
    opt.Password.RequireUppercase = false; //b�y�k harf zorunlulu�u

}).AddRoleManager<RoleManager<AppRole>>().
AddErrorDescriber<CustomIdentityErrorDescriber>().
AddEntityFrameworkStores<AppDbContext>().
AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = new PathString("/Admin/Auth/Login");
    config.LogoutPath = new PathString("/Admin/Auth/Login");
    config.Cookie = new CookieBuilder
    {
        Name = "MyBlog",
        HttpOnly = true,
        SameSite = SameSiteMode.Strict,
        SecurePolicy = CookieSecurePolicy.SameAsRequest  //canl�da alway olan� se�
    };
    config.SlidingExpiration = true;
    config.ExpireTimeSpan = TimeSpan.FromDays(7); //kullan�c� bilgileri 7 g�n boyunda kay�tl� kalacak bilgi girmeden direk admin paneline girmek gibi
    config.AccessDeniedPath = new PathString("/Admin/Auth/AccessDenied");  //yetkisini belirliyoruz 
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseNToastNotify();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();




app.UseAuthentication();//ilk bu olmal� 
app.UseAuthorization();//arkas�na bu gelmeli

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
     name: "Admin",
     areaName: "Admin",
     pattern: "Admin/{Controller=Home}/{action=Index}/{id?}");
    endpoints.MapDefaultControllerRoute();
});


app.Run();
