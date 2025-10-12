using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels.VM;

public class AddOrUpdateStationaryStoreVm
{
    public int? Id { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    [Required]
    public int AddressId { get; set; }
}

