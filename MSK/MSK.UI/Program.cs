using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MSK.Business.DTOs.HomeSlideDTOs;
using MSK.Business.Mappers;
using MSK.Core.Models;
using MSK.Data.DAL;
using MSK.Data.RepositoryRegistrations;
using Pigga.Business.ServiceRegistrations;

var builder = WebApplication.CreateBuilder(args);
const string connection = "default";
// Add services to the container.
builder.Services.AddControllersWithViews().AddFluentValidation(opt =>
{
    opt.RegisterValidatorsFromAssembly(typeof(HomeSlideCreateDtoValidator).Assembly);
});
builder.Services.AddDbContext<AppDbContext>(opts =>
{

    opts.UseSqlServer(builder.Configuration.GetConnectionString(connection));
});
builder.Services.AddIdentity<Voter, IdentityRole>(opts =>
{
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequiredLength = 6;
    opts.Password.RequireUppercase = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireDigit = false;
   
}).AddEntityFrameworkStores<AppDbContext>().
                AddDefaultTokenProviders();

builder.Services.AddIdentityCore<User>(opts =>
{
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequiredLength = 8;
    opts.Password.RequireUppercase = true;
    opts.Password.RequireDigit = false;
    opts.SignIn.RequireConfirmedEmail = true;

}).AddRoles<IdentityRole>().
                AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<User, IdentityRole>>().
                AddEntityFrameworkStores<AppDbContext>().
                AddDefaultTokenProviders();
               

builder.Services.AddAutoMapper(typeof(MapProfile).Assembly);
builder.Services.RegisterServices(builder.Configuration);
builder.Services.RegisterRepos();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Manage/account/login";

});

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
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
