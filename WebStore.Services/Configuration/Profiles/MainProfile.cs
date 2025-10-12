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
        // Address
        CreateMap<AddOrUpdateAddressVm, Address>();
        CreateMap<Address, AddressVm>();

        // Store
        CreateMap<AddOrUpdateStationaryStoreVm, StationaryStore>();
        CreateMap<StationaryStore, StationaryStoreVm>()
            .ForMember(d => d.EmployeesCount, o => o.MapFrom(s => s.Employees.Count))
            .ForMember(d => d.TotalStockQuantity, o => o.MapFrom(s => s.ProductStocks.Sum(ps => ps.Quantity)));

        // Order & items
        CreateMap<AddOrUpdateOrderItemVm, OrderProduct>();
        CreateMap<OrderProduct, OrderItemVm>()
            .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.Name))
            .ForMember(d => d.LineTotal, o => o.MapFrom(s => s.UnitPrice * s.Quantity));
        CreateMap<Order, OrderVm>()
            .ForMember(d => d.Items, o => o.MapFrom(s => s.OrderProducts))
            .ForMember(d => d.Total, o => o.MapFrom(s => s.OrderProducts.Sum(i => i.UnitPrice * i.Quantity)));

        // Invoice
        CreateMap<AddOrUpdateInvoiceVm, Invoice>()
            .ForMember(d => d.IssuedAt, o => o.MapFrom(s => s.IssuedAt ?? DateTime.UtcNow));
        CreateMap<Invoice, InvoiceVm>()
            .ForMember(d => d.OrderIds, o => o.MapFrom(s => s.Orders.Select(o2 => o2.Id)));

}
}