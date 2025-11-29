namespace WebStore.Model.DataModels;


public class Category
{
public int Id { get; set; }
public string Name { get; set; } = default!;
public string? Tag { get; set; }
public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}