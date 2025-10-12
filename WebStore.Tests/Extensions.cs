using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WebStore.DAL.EF;
using WebStore.Model.DataModels;


namespace WebStore.Tests;


public static class Extensions
{
public static async void SeedData(this IServiceCollection services)
{
var sp = services.BuildServiceProvider();
var db = sp.GetRequiredService<ApplicationDbContext>();
var userManager = sp.GetRequiredService<UserManager<User>>();


// Supplier
var supplier1 = new Supplier
{
Id = 1,
FirstName = "Adam",
LastName = "Bednarski",
UserName = "supp1@eg.eg",
Email = "supp1@eg.eg",
RegistrationDate = new DateTime(2010, 1, 1)
};
await userManager.CreateAsync(supplier1, "User1234");


// Category
var cat1 = new Category { Id = 1, Name = "Computers", Tag = "#computer" };
await db.AddAsync(cat1);


// Products
var p1 = new Product
{
Id = 1,
CategoryId = cat1.Id,
SupplierId = supplier1.Id,
Description = "Bardzo praktyczny monitor 32 cale",
ImageBytes = new byte[] { 0xff, 0xff, 0xff, 0x80 },
Name = "Monitor Dell 32",
Price = 1000,
Weight = 20
};
await db.AddAsync(p1);


var p2 = new Product
{
Id = 2,
CategoryId = cat1.Id,
SupplierId = supplier1.Id,
Description = "Precyzyjna mysz do pracy",
ImageBytes = new byte[] { 0xff, 0xff, 0xff, 0x70 },
Name = "Mysz Logitech",
Price = 500,
Weight = 0.5f
};
await db.AddAsync(p2);


await db.SaveChangesAsync();
}
}