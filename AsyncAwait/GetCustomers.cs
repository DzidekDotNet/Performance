using System.Collections.Concurrent;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Reports;
BenchmarkRunner.Run<GetCustomersBenchmark>(new DebugInProcessConfig());

[MemoryDiagnoser]
[RankColumn]
[Config(typeof(Config))]
// [SimpleJob(RuntimeMoniker.Net60, baseline:true)]
// [SimpleJob(RuntimeMoniker.Net70)]
// [SimpleJob(RuntimeMoniker.Net80)]
public class GetCustomersBenchmark
{
  private TaskService _customerService = new TaskService();
  
  // [Benchmark(Baseline = true)]
  // public async Task<Customer> GetCustomers_WithAsync()
  // {
  //   return await _customerService.Get(CancellationToken.None);
  // }
  //
  // [Benchmark]
  // public Task<Customer> GetCustomers_WithoutAsync()
  // {
  //   return _customerService.Get(CancellationToken.None);
  // }
  
  [Benchmark]
  public Task<Customer> GetCustomers()
  {
    return GetCustomersFromService();
    
    async Task<Customer> GetCustomersFromService()
    {
      return await _customerService.Get(CancellationToken.None);
    }
  }

  private class Config : ManualConfig
  {
    public Config()
    {
      SummaryStyle = SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend);
    }
  }
  
}

internal sealed class TaskService
{
  private readonly ConcurrentDictionary<int, Customer> _cache = new();
  private readonly TaskRepository _repo = new TaskRepository();
  internal async Task<Customer> Get(CancellationToken cancellationToken)
  {
    if (!_cache.ContainsKey(1))
    {
      var customer = await _repo.Get(cancellationToken);
      _cache.TryAdd(1, customer);
    }
    return _cache[1];
  }
}

internal sealed class TaskRepository
{
  internal Task<Customer> Get(CancellationToken cancellationToken)
  {
    return Task.FromResult(new Customer(1, "name", 1000));
  }
}

public class Customer
{
  public int Id { get; set; }
  public string Name { get; set; }
  public long Revenue { get; set; }

  public Customer(int id, string name, long revenue)
  {
    Id = id;
    Name = name;
    Revenue = revenue;
  }
}