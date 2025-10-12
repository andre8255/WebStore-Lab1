using AutoMapper;
using WebStore.Model.DataModels;
using WebStore.ViewModels.VM;


namespace WebStore.Services.Configuration.Profiles;


public class MainProfile : Profile
{
public MainProfile()
{
CreateMap<AddOrUpdateProductVm, Product>();
CreateMap<Product, ProductVm>()
.ForMember(d => d.Quantity, o => o.MapFrom(s => s.ProductStocks.Sum(ps => ps.Quantity)));
}
}