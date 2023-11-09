using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkWithMediatR;

public class TestContext: DbContext
{
  public TestContext(DbContextOptions<TestContext> options) : base(options)
  {
            
  }
  public TestContext() : base()
  {
            
  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlServer("server=localhost; database=test; user=sa; password=P@ssw0rd;TrustServerCertificate=True");
    base.OnConfiguring(optionsBuilder);
  }

  public DbSet<CustomerEntity> Customers { get; set; }
  public DbSet<AddressEntity> Addresses { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.Entity<CustomerEntity>(build =>
    {
      build.HasKey(entry => entry.Id);
      build.Property(entry => entry.Id).ValueGeneratedOnAdd();
      build.HasMany(entry => entry.Addresses).WithOne().HasForeignKey(entity => entity.CustomerId);
    });
    
    modelBuilder.Entity<AddressEntity>(build =>
    {
      build.HasKey(entry => entry.Id);
      build.Property(entry => entry.Id).ValueGeneratedOnAdd();
    });
  }
}

public class CustomerEntity
{
  public int Id { get; set; }
  public string Name { get; set; }
  public long Revenue { get; set; }
  public List<AddressEntity> Addresses { get; set; }
}

public class AddressEntity
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string City { get; set; }
  public string Code { get; set; }
  public string Country { get; set; }
  public int CustomerId { get; set; }
}

