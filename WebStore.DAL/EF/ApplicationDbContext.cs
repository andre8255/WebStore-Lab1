using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebStore.Model.DataModels;

namespace WebStore.DAL.EF;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductStock> ProductStocks => Set<ProductStock>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderProduct> OrderProducts => Set<OrderProduct>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<StationaryStore> StationaryStores => Set<StationaryStore>();
    public DbSet<StationaryStoreEmployee> StationaryStoreEmployees => Set<StationaryStoreEmployee>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<Customer> Customers => Set<Customer>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder b)
    {
        base.OnModelCreating(b);

        // TPH – dziedziczenie User
        b.Entity<User>()
            .HasDiscriminator<string>("UserType")
            .HasValue<User>("User")
            .HasValue<Customer>("Customer")
            .HasValue<Supplier>("Supplier")
            .HasValue<StationaryStoreEmployee>("StationaryStoreEmployee");

        // 1:M Customer–Order
        b.Entity<Order>()
            .HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        // 1:M Supplier–Product
        b.Entity<Product>()
            .HasOne(p => p.Supplier)
            .WithMany(s => s.Products)
            .HasForeignKey(p => p.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);

        // 1:M Product–ProductStock
        b.Entity<ProductStock>()
            .HasOne(ps => ps.Product)
            .WithMany(p => p.ProductStocks)
            .HasForeignKey(ps => ps.ProductId);

        // 1:M Customer–Address (nullable FK)
        b.Entity<Customer>()
            .HasOne(c => c.Address)
            .WithMany(a => a.Customers)
            .HasForeignKey(c => c.AddressId)
            .OnDelete(DeleteBehavior.SetNull);

        // 1:M Category–Product
        b.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);

        // 1:M Invoice–Order
        b.Entity<Order>()
            .HasOne(o => o.Invoice)
            .WithMany(i => i.Orders)
            .HasForeignKey(o => o.InvoiceId)
            .OnDelete(DeleteBehavior.SetNull);

        // 1:M StationaryStore–Address
        b.Entity<StationaryStore>()
            .HasOne(s => s.Address)
            .WithMany(a => a.Stores)
            .HasForeignKey(s => s.AddressId);

        // 1:M StationaryStore–StationaryStoreEmployee
        // ZMIANA TUTAJ: Dodano OnDelete(DeleteBehavior.Restrict)
        b.Entity<StationaryStoreEmployee>()
            .HasOne(e => e.StationaryStore)
            .WithMany(s => s.Employees)
            .HasForeignKey(e => e.StationaryStoreId)
            .OnDelete(DeleteBehavior.Restrict); 

        // N:M Order–Product (encja asocjacyjna)
        b.Entity<OrderProduct>().HasKey(op => new { op.OrderId, op.ProductId });
        b.Entity<OrderProduct>()
            .HasOne(op => op.Order)
            .WithMany(o => o.OrderProducts)
            .HasForeignKey(op => op.OrderId);
        b.Entity<OrderProduct>()
            .HasOne(op => op.Product)
            .WithMany(p => p.OrderProducts)
            .HasForeignKey(op => op.ProductId);

        // Decimal precision
        b.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(18,2)");
        b.Entity<OrderProduct>().Property(op => op.UnitPrice).HasColumnType("decimal(18,2)");
    }
}