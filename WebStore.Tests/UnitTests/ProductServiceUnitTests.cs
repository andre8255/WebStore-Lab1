using WebStore.DAL.EF;
using WebStore.Services.Interfaces;
using WebStore.ViewModels.VM;
using Xunit;


namespace WebStore.Tests.UnitTests;


public class ProductServiceUnitTests : BaseUnitTests
{
private readonly IProductService _svc;


public ProductServiceUnitTests(ApplicationDbContext dbContext, IProductService svc) : base(dbContext)
=> _svc = svc;


[Fact]
public void GetProductTest()
{
var product = _svc.GetProduct(p => p.Name == "Monitor Dell 32");
Assert.NotNull(product);
}


[Fact]
public void GetMultipleProductsTest()
{
var products = _svc.GetProducts(p => p.Id >= 1 && p.Id <= 2).ToList();
Assert.NotEmpty(products);
Assert.Equal(2, products.Count);
}


[Fact]
public void AddNewProductTest()
{
var vm = new AddOrUpdateProductVm
{
Name = "MacBook Pro",
CategoryId = 1,
SupplierId = 1,
ImageBytes = new byte[] { 0xff, 0xff, 0xff, 0x80 },
Price = 6000,
Weight = 1.1f,
Description = "MacBook Pro z procesorem M1 8GB RAM, Dysk 256 GB"
};
var created = _svc.AddOrUpdateProduct(vm);
Assert.NotNull(created);
Assert.Equal("MacBook Pro", created.Name);
}


[Fact]
public void UpdateProductTest()
{
var vm = new AddOrUpdateProductVm
{
Id = 1,
Description = "Bardzo praktyczny monitor 32 cale",
ImageBytes = new byte[] { 0xff, 0xff, 0xff, 0x80 },
Name = "Monitor Dell 32",
Price = 2000,
Weight = 20,
CategoryId = 1,
SupplierId = 1
};
var edited = _svc.AddOrUpdateProduct(vm);
Assert.NotNull(edited);
Assert.Equal("Monitor Dell 32", edited.Name);
Assert.Equal(2000, edited.Price);
}
}