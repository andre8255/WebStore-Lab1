namespace WebStore.ViewModels.VM;

public class OrderVm
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public int CustomerId { get; set; }
    public int? InvoiceId { get; set; }

    public List<OrderItemVm> Items { get; set; } = new();

    // Suma pozycji, wypełniana przez AutoMapper
    public decimal Total { get; set; }
}
