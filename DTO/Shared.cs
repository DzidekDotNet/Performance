namespace DTO;

internal sealed class Customer
{
  public int Id { get; set; }
  public string Name { get; set; }
  public long Revenue { get; set; }
}

internal sealed class CustomerClassDTO
{
  public int Id { get; set; }
  public string Name { get; set; }
  public long Revenue { get; set; }
  
  public CustomerClassDTO(Customer customer)
  {
    Id = customer.Id;
    Name = customer.Name;
    Revenue = customer.Revenue;
  }
}

internal sealed record CustomerRecordDTO
{
  public int Id { get; init; }
  public string Name { get; init; }
  public long Revenue { get; init; }
  
  public CustomerRecordDTO(Customer customer)
  {
    Id = customer.Id;
    Name = customer.Name;
    Revenue = customer.Revenue;
  }
}

internal struct CustomerStructDTO
{
  public int Id { get; set; }
  public string Name { get; set; }
  public long Revenue { get; set; }
  
  public CustomerStructDTO(Customer customer)
  {
    Id = customer.Id;
    Name = customer.Name;
    Revenue = customer.Revenue;
  }
}

internal record struct CustomerRecordStructDTO
{
  public int Id { get; init; }
  public string Name { get; init; }
  public long Revenue { get; init; }
  
  public CustomerRecordStructDTO(Customer customer)
  {
    Id = customer.Id;
    Name = customer.Name;
    Revenue = customer.Revenue;
  }
}

internal sealed class Repository
{
  private static IList<Customer> _customers = new List<Customer>();
  public Repository()
  {
    if (!_customers.Any())
    {
      Random random = new Random();
      for (int i = 0; i < 25; i++)
      {
        _customers.Add(new Customer()
        {
          Id = i,
          Name = $"customer {i}",
          Revenue = random.Next(i * 1000, i * 50000)
        });
      }
    }
  }
  internal IEnumerable<Customer> GetCustomers() => _customers;
}
