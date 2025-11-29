namespace WebStore.Model.DataModels;


public class Order
{
public int Id { get; set; }
public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
public int CustomerId { get; set; }
public virtual  Customer Customer { get; set; } = default!;


public int? InvoiceId { get; set; }
public virtual Invoice? Invoice { get; set; }


public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}