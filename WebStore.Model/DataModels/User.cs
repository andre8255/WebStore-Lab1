using Microsoft.AspNetCore.Identity;


namespace WebStore.Model.DataModels;


public class User : IdentityUser<int>
{
public string? FirstName { get; set; }
public string? LastName { get; set; }
public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
}


public class Customer : User
{
public Address? Address { get; set; }
public int? AddressId { get; set; }
public ICollection<Order> Orders { get; set; } = new List<Order>();
}


public class Supplier : User
{
public ICollection<Product> Products { get; set; } = new List<Product>();
}


public class StationaryStoreEmployee : User
{
public int StationaryStoreId { get; set; }
public StationaryStore StationaryStore { get; set; } = default!;
}