using Microsoft.EntityFrameworkCore;
using ShopFlower.Data;
using ShopFlower.Service;
using ShopFlower.IService.ServiceUser;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.AspNetCore.Authentication.Google;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MyDatabaseConnection")));

// ����������� �������
builder.Services.AddTransient<IServiceUser,ServiceUser>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index";  // ��������� ���� ��� ��������������� ��� ��������� ��������������
        options.AccessDeniedPath = "/Login/AccessDenied";  // ��������� ���� ��� ������� ��� ������
    })
    .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
    {
        options.ClientId = builder.Configuration.GetSection("Google:ClientId").Value;
        options.ClientSecret = builder.Configuration.GetSection("Google:ClientSecret").Value;
        options.Scope.Add("email");
    });

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",

    pattern: "{controller=Home}/{action=Index}/{id?}");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
