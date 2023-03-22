using Microsoft.EntityFrameworkCore;
using Blog.Data.Context;
using Blog.Data.Extensions;
using Blog.Service.Services;
using Blog.Service.Extensions;

var builder = WebApplication.CreateBuilder(args);

//program.cs kirletmeden extension ile ýrepsort çaðýrdýðýmýzda repostroy de çaðýrýyoruz
builder.Services.LoadDataLayerExtension(builder.Configuration);

builder.Services.LoadServiceLayerExtension();


// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation(); //razor ile viewde run time sýrasýnda deðiþikleri görmemizi saðlýyor






var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

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
