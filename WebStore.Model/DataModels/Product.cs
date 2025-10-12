namespace WebStore.Model.DataModels;


public class Product
{
public int Id { get; set; }
public string Name { get; set; } = default!;
public string Description { get; set; } = default!;
public byte[] ImageBytes { get; set; } = default!;
public decimal Price { get; set; }
public float Weight { get; set; }


public int CategoryId { get; set; }
public Category Category { get; set; } = default!;


public int SupplierId { get; set; }
public Supplier Supplier { get; set; } = default!;


public ICollection<ProductStock> ProductStocks { get; set; } = new List<ProductStock>();
public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}