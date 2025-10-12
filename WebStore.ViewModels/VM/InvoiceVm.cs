namespace WebStore.ViewModels.VM;

public class InvoiceVm
{
    public int Id { get; set; }
    public string Number { get; set; } = default!;
    public DateTime IssuedAt { get; set; }

    // uproszczona reprezentacja powiązań
    public List<int> OrderIds { get; set; } = new();
}

