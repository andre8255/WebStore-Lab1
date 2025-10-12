using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebStore.DAL.EF;
using WebStore.Model.DataModels;
using WebStore.Services.ConcreteServices;
using WebStore.Services.Configuration.Profiles;
using WebStore.Services.Interfaces;
using AutoMapper;

namespace WebStore.Tests;


public class Startup
{
public void ConfigureServices(IServiceCollection services)
{
    var _ = typeof(MapperConfiguration);
services.AddAutoMapper(typeof(MainProfile));
services.AddEntityFrameworkInMemoryDatabase()
.AddDbContext<ApplicationDbContext>(o => o.UseInMemoryDatabase("InMemoryDb"));


services.AddIdentity<User, IdentityRole<int>>(opt =>
{
opt.SignIn.RequireConfirmedAccount = false;
opt.Password.RequiredLength = 6;
opt.Password.RequiredUniqueChars = 0;
opt.Password.RequireNonAlphanumeric = false;
})
.AddRoleManager<RoleManager<IdentityRole<int>>>()
.AddUserManager<UserManager<User>>()
.AddEntityFrameworkStores<ApplicationDbContext>();


services.AddTransient(typeof(ILogger), typeof(Logger<Startup>));
services.AddTransient<IProductService, ProductService>();


services.SeedData();
}
}