using Microsoft.EntityFrameworkCore;
using StorageManagementApp;
using StorageManagementApp.Mvc.Services;
using StorageManagementApp.Mvc.Services.Interfaces;
using AutoMapper;
using StorageManagementApp.Mvc;
using StorageManagementApp.Contracts.Guards;

var builder = WebApplication.CreateBuilder(args);


builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.AddFile("Logs\\\\log-{Date}.txt", LogLevel.Information);
});
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<StorageDBContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("conn")));

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
    app.UseExceptionHandler("/Home/Error");
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
