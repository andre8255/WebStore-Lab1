using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using WebStore.DAL.EF;
using WebStore.Model.DataModels;
using WebStore.Services.Interfaces;
using WebStore.ViewModels.VM;

namespace WebStore.Services.ConcreteServices;

public class AddressService : BaseService, IAddressService
{
    public AddressService(ApplicationDbContext db, IMapper mapper, ILogger<AddressService> logger)
        : base(db, mapper, logger) { }

    public AddressVm AddOrUpdateAddress(AddOrUpdateAddressVm vm)
    {
        try
        {
            if (vm is null) throw new ArgumentNullException(nameof(vm));
            var entity = Mapper.Map<Address>(vm);
            if (vm.Id.HasValue && vm.Id.Value != 0)
                DbContext.Addresses.Update(entity);
            else
                DbContext.Addresses.Add(entity);

            DbContext.SaveChanges();
            return Mapper.Map<AddressVm>(entity);
        }
        catch (Exception ex) { Logger.LogError(ex, ex.Message); throw; }
    }

    public AddressVm? GetAddress(Expression<Func<Address, bool>> filter)
    {
        var e = DbContext.Addresses.FirstOrDefault(filter);
        return e is null ? null : Mapper.Map<AddressVm>(e);
    }

    public IEnumerable<AddressVm> GetAddresses(Expression<Func<Address, bool>>? filter = null)
    {
        var q = DbContext.Addresses.AsQueryable();
        if (filter != null) q = q.Where(filter);
        return Mapper.Map<IEnumerable<AddressVm>>(q.ToList());
    }
}
