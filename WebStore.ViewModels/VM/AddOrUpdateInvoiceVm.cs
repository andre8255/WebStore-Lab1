using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels.VM;

public class AddOrUpdateInvoiceVm
{
    public int? Id { get; set; }

    [Required]
    public string Number { get; set; } = default!;

    public DateTime? IssuedAt { get; set; }

    // Id zamówień do podpięcia
    public List<int> OrderIds { get; set; } = new();
}
