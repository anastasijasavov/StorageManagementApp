using Microsoft.EntityFrameworkCore;
using StorageManagementApp;
using StorageManagementApp.Mvc.Services;
using StorageManagementApp.Mvc.Services.Interfaces;
using AutoMapper;
using StorageManagementApp.Mvc;
using StorageManagementApp.Contracts.Guards;
using StorageManagementApp.Contracts.ExceptionHandler;
using StorageManagementApp.Models;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);


builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.AddFile("Logs\\log-{Date}.txt", LogLevel.Information);
});
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<StorageDBContext>(opt =>
{
    opt.EnableSensitiveDataLogging();
    opt.UseSqlServer(builder.Configuration.GetConnectionString("conn"));
});

builder.Services.AddDefaultIdentity<User>(opts =>
{
    opts.SignIn.RequireConfirmedAccount = false;
    opts.Password.RequireDigit = false;
    opts.SignIn.RequireConfirmedEmail = false;
    opts.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_";
}).AddEntityFrameworkStores<StorageDBContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(5);
    options.LoginPath = "/User/Login";
});
builder.Services.AddLogging();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddCors(options =>
{
    //TODO: see if MVC has it's own system of reading these from config
    options.AddPolicy("allowAnyOriginPolicy", corsOptions =>
    {
        corsOptions.AllowAnyHeader();
        corsOptions.AllowAnyMethod();
        corsOptions.AllowAnyOrigin();
    });
});
builder.Services.AddScoped<StorageDBContext>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<PrivateGuardAttribute>();

//automapper injection
var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperProfile());
});
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseMiddleware<StorageManagementExceptionMiddleware>();
    //app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
