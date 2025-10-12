namespace WebStore.Model.DataModels;


public class Address
{
public int Id { get; set; }
public string Country { get; set; } = default!;
public string City { get; set; } = default!;
public string Street { get; set; } = default!;
public string ZipCode { get; set; } = default!;
public string? ApartmentNo { get; set; }


public ICollection<Customer> Customers { get; set; } = new List<Customer>();
public ICollection<StationaryStore> Stores { get; set; } = new List<StationaryStore>();
}