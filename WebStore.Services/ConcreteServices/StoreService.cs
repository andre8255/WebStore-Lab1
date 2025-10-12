using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebStore.DAL.EF;
using WebStore.Model.DataModels;
using WebStore.Services.Interfaces;
using WebStore.ViewModels.VM;

namespace WebStore.Services.ConcreteServices;

public class StoreService : BaseService, IStoreService
{
    public StoreService(ApplicationDbContext db, IMapper mapper, ILogger<StoreService> logger)
        : base(db, mapper, logger) { }

    public StationaryStoreVm AddOrUpdateStore(AddOrUpdateStationaryStoreVm vm)
    {
        try
        {
            if (vm is null) throw new ArgumentNullException(nameof(vm));
            var entity = Mapper.Map<StationaryStore>(vm);
            if (vm.Id.HasValue && vm.Id.Value != 0)
                DbContext.StationaryStores.Update(entity);
            else
                DbContext.StationaryStores.Add(entity);

            DbContext.SaveChanges();
            return Mapper.Map<StationaryStoreVm>(entity);
        }
        catch (Exception ex) { Logger.LogError(ex, ex.Message); throw; }
    }

    public StationaryStoreVm? GetStore(Expression<Func<StationaryStore, bool>> filter)
    {
        var e = DbContext.StationaryStores
            .Include(s => s.Employees)
            .Include(s => s.ProductStocks)
            .FirstOrDefault(filter);

        return e is null ? null : Mapper.Map<StationaryStoreVm>(e);
    }

    public IEnumerable<StationaryStoreVm> GetStores(Expression<Func<StationaryStore, bool>>? filter = null)
    {
        var q = DbContext.StationaryStores
            .Include(s => s.Employees)
            .Include(s => s.ProductStocks)
            .AsQueryable();

        if (filter != null) q = q.Where(filter);
        return Mapper.Map<IEnumerable<StationaryStoreVm>>(q.ToList());
    }
}
