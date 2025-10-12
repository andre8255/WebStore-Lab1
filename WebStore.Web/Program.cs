using Microsoft.EntityFrameworkCore;
using WebStore.DAL.EF;
using WebStore.Services.Configuration.Profiles;
using WebStore.Services.Interfaces;
using WebStore.Services.ConcreteServices;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<ApplicationDbContext>(opt =>
opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
.UseLazyLoadingProxies());


builder.Services.AddAutoMapper(typeof(MainProfile));


// DI serwisów
builder.Services.AddScoped<IProductService, ProductService>();


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
app.UseExceptionHandler("/Home/Error");
app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();
app.MapFallbackToFile("index.html");
app.Run();