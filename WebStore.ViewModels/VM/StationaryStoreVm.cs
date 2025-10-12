namespace WebStore.ViewModels.VM;

public class StationaryStoreVm
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int AddressId { get; set; }

    // Pola agregowane – wypełniane przez AutoMapper
    public int EmployeesCount { get; set; }
    public int TotalStockQuantity { get; set; }
}
