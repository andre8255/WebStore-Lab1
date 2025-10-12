using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebStore.DAL.EF;
using WebStore.Model.DataModels;
using WebStore.Services.Interfaces;
using WebStore.ViewModels.VM;

namespace WebStore.Services.ConcreteServices;

public class OrderService : BaseService, IOrderService
{
    public OrderService(ApplicationDbContext db, IMapper mapper, ILogger<OrderService> logger)
        : base(db, mapper, logger) { }

    public OrderVm AddOrUpdateOrder(AddOrUpdateOrderVm vm)
    {
        try
        {
            if (vm is null) throw new ArgumentNullException(nameof(vm));

            Order entity;
            if (vm.Id.HasValue && vm.Id.Value != 0)
            {
                entity = DbContext.Orders
                    .Include(o => o.OrderProducts)
                    .First(o => o.Id == vm.Id.Value);

                entity.CustomerId = vm.CustomerId;
                entity.InvoiceId = vm.InvoiceId;

                DbContext.OrderProducts.RemoveRange(entity.OrderProducts);
                entity.OrderProducts = vm.Items.Select(i => new OrderProduct
                {
                    OrderId = entity.Id,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList();

                DbContext.Orders.Update(entity);
            }
            else
            {
                entity = new Order
                {
                    CustomerId = vm.CustomerId,
                    InvoiceId = vm.InvoiceId,
                    OrderProducts = vm.Items.Select(i => new OrderProduct
                    {
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice
                    }).ToList()
                };

                DbContext.Orders.Add(entity);
            }

            DbContext.SaveChanges();

            var loaded = DbContext.Orders
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .First(o => o.Id == entity.Id);

            return Mapper.Map<OrderVm>(loaded);
        }
        catch (Exception ex) { Logger.LogError(ex, ex.Message); throw; }
    }

    public OrderVm? GetOrder(Expression<Func<Order, bool>> filter)
    {
        var e = DbContext.Orders
            .Include(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .FirstOrDefault(filter);

        return e is null ? null : Mapper.Map<OrderVm>(e);
    }

    public IEnumerable<OrderVm> GetOrders(Expression<Func<Order, bool>>? filter = null)
    {
        var q = DbContext.Orders
            .Include(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .AsQueryable();

        if (filter != null) q = q.Where(filter);
        return Mapper.Map<IEnumerable<OrderVm>>(q.ToList());
    }
}
