using System.Linq.Expressions;
using WebStore.Model.DataModels;
using WebStore.ViewModels.VM;

namespace WebStore.Services.Interfaces;

public interface IStoreService
{
    StationaryStoreVm AddOrUpdateStore(AddOrUpdateStationaryStoreVm vm);
    StationaryStoreVm? GetStore(Expression<Func<StationaryStore, bool>> filter);
    IEnumerable<StationaryStoreVm> GetStores(Expression<Func<StationaryStore, bool>>? filter = null);
}
