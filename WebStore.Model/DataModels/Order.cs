namespace WebStore.Model.DataModels;


public class Order
{
public int Id { get; set; }
public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
public int CustomerId { get; set; }
public Customer Customer { get; set; } = default!;


public int? InvoiceId { get; set; }
public Invoice? Invoice { get; set; }


public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}