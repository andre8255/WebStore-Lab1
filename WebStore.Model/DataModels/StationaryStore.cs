namespace WebStore.Model.DataModels;


public class StationaryStore
{
public int Id { get; set; }
public string Name { get; set; } = default!;


public int AddressId { get; set; }
public Address Address { get; set; } = default!;


public ICollection<StationaryStoreEmployee> Employees { get; set; } = new List<StationaryStoreEmployee>();
public ICollection<ProductStock> ProductStocks { get; set; } = new List<ProductStock>();
}