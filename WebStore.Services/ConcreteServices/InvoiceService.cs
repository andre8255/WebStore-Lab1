using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebStore.DAL.EF;
using WebStore.Model.DataModels;
using WebStore.Services.Interfaces;
using WebStore.ViewModels.VM;

namespace WebStore.Services.ConcreteServices;

public class InvoiceService : BaseService, IInvoiceService
{
    public InvoiceService(ApplicationDbContext db, IMapper mapper, ILogger<InvoiceService> logger)
        : base(db, mapper, logger) { }

    public InvoiceVm AddOrUpdateInvoice(AddOrUpdateInvoiceVm vm)
    {
        try
        {
            if (vm is null) throw new ArgumentNullException(nameof(vm));

            Invoice entity;

            if (vm.Id.HasValue && vm.Id.Value != 0)
            {
                entity = DbContext.Invoices.Include(i => i.Orders).First(i => i.Id == vm.Id.Value);

                entity.Number = vm.Number;
                if (vm.IssuedAt.HasValue) entity.IssuedAt = vm.IssuedAt.Value;

                // odczep stare
                foreach (var o in entity.Orders) o.InvoiceId = null;
                DbContext.SaveChanges();

                // podepnij nowe
                var orders = DbContext.Orders.Where(o => vm.OrderIds.Contains(o.Id)).ToList();
                foreach (var o in orders) o.InvoiceId = entity.Id;
                DbContext.SaveChanges();
            }
            else
            {
                entity = Mapper.Map<Invoice>(vm);
                DbContext.Invoices.Add(entity);
                DbContext.SaveChanges();

                var orders = DbContext.Orders.Where(o => vm.OrderIds.Contains(o.Id)).ToList();
                foreach (var o in orders) o.InvoiceId = entity.Id;
                DbContext.SaveChanges();
            }

            var loaded = DbContext.Invoices.Include(i => i.Orders).First(i => i.Id == entity.Id);
            return Mapper.Map<InvoiceVm>(loaded);
        }
        catch (Exception ex) { Logger.LogError(ex, ex.Message); throw; }
    }

    public InvoiceVm? GetInvoice(Expression<Func<Invoice, bool>> filter)
    {
        var e = DbContext.Invoices.Include(i => i.Orders).FirstOrDefault(filter);
        return e is null ? null : Mapper.Map<InvoiceVm>(e);
    }

    public IEnumerable<InvoiceVm> GetInvoices(Expression<Func<Invoice, bool>>? filter = null)
    {
        var q = DbContext.Invoices.Include(i => i.Orders).AsQueryable();
        if (filter != null) q = q.Where(filter);
        return Mapper.Map<IEnumerable<InvoiceVm>>(q.ToList());
    }
}

