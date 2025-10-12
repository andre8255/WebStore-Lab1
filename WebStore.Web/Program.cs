using Microsoft.EntityFrameworkCore;
using WebStore.DAL.EF;
using WebStore.Services.Configuration.Profiles;
using WebStore.Services.Interfaces;
using WebStore.Services.ConcreteServices;

var builder = WebApplication.CreateBuilder(args);

// MVC + Swagger
builder.Services.AddControllers();                 // wystarczy AddControllers (bez duplikatu)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core (lazy loading + SQL Server)
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseLazyLoadingProxies()                   // najpierw włącz proxye
       .UseSqlServer(builder.Configuration.GetConnectionString("Default"))
);

// AutoMapper + DI serwisów
builder.Services.AddAutoMapper(typeof(MainProfile));
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

// ✅ W DEV włącz Swagger (albo włącz zawsze, jeśli wolisz)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Jeśli chcesz zawsze: odkomentuj poniższe dwie linie i usuń if-a:
// app.UseSwagger();
// app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
