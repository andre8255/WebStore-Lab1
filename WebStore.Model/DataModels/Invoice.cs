namespace WebStore.Model.DataModels;


public class Invoice
{
public int Id { get; set; }
public string Number { get; set; } = default!;
public DateTime IssuedAt { get; set; } = DateTime.UtcNow;


public virtual ICollection<Order> Orders { get; set; } = new List<Order>(); // 1:M Invoice–Order
}