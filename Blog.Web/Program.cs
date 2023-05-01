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

//program.cs kirletmeden extension ile ýrepsort çaðýrdýðýmýzda repostroy de çaðýrýyoruz
builder.Services.LoadDataLayerExtension(builder.Configuration);

builder.Services.LoadServiceLayerExtension();

builder.Services.AddSession();


// Add services to the container.
builder.Services.AddControllersWithViews().AddNToastNotifyToastr(new NToastNotify.ToastrOptions
{
    PositionClass=ToastPositions.TopRight, //sað tarfata çýkacak
    TimeOut=3000  //3 saniye duracak
    
}).AddRazorRuntimeCompilation(); //razor ile viewde run time sýrasýnda deðiþikleri görmemizi saðlýyor


builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireLowercase = false;  //küçük harf zorunluluðu
    opt.Password.RequireUppercase = false; //büyük harf zorunluluðu

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
        SecurePolicy = CookieSecurePolicy.SameAsRequest  //canlýda alway olaný seç
    };
    config.SlidingExpiration = true;
    config.ExpireTimeSpan = TimeSpan.FromDays(7); //kullanýcý bilgileri 7 gün boyunda kayýtlý kalacak bilgi girmeden direk admin paneline girmek gibi
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




app.UseAuthentication();//ilk bu olmalý 
app.UseAuthorization();//arkasýna bu gelmeli

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
