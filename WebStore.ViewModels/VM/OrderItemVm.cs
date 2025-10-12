namespace WebStore.ViewModels.VM;

public class OrderItemVm
{
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    // Wyliczane po stronie mapowania
    public decimal LineTotal { get; set; }
}

