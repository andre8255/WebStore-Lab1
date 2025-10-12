using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels.VM;

public class AddOrUpdateOrderVm
{
    public int? Id { get; set; }

    [Required]
    public int CustomerId { get; set; }

    public int? InvoiceId { get; set; }

    [Required]
    public List<AddOrUpdateOrderItemVm> Items { get; set; } = new();
}
