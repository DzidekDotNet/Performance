using System.Collections.Concurrent;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Reports;
using EntityFrameworkWithMediatR;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
BenchmarkRunner.Run<GetCustomersBenchmark>(/*new DebugInProcessConfig()*/);

[MemoryDiagnoser]
[RankColumn]
[Config(typeof(Config))]
// [SimpleJob(RuntimeMoniker.Net60, baseline:true)]
// [SimpleJob(RuntimeMoniker.Net70)]
// [SimpleJob(RuntimeMoniker.Net80)]
public class GetCustomersBenchmark
{
  [Params(1000/*, 10000, 250000*/)]
  public int CustomersCount { get; set; }
  
  [Params(10/*, 25, 100*/)]
  public int CustomersTake { get; set; }
  //
  // [Params(1, 100 /*, 10000*/)]
  // public int Iterations { get; set; }

  private static int counter = 1;
  private TaskService _taskService = new TaskService();
  private ValueTaskService _valueTaskService = new ValueTaskService();
  
  [GlobalSetup]
  public void Setup()
  {
    var services = new ServiceCollection();
    services.AddMediatR(typeof(Benchmark));
    services.AddDbContext<TestContext>(options =>
    {
      options.UseSqlServer("server=localhost; database=test; user=sa; password=P@ssw0rd;TrustServerCertificate=True");
    });

    var provider = services.BuildServiceProvider();

    SampleData.Initialize(provider, CustomersCount);
  }
  
  [Benchmark(Baseline = true)]
  public async Task<Customer> GetCustomers_Task()
  {
    return await _taskService.Get(1, CustomersTake, CancellationToken.None);
  }
  
  [Benchmark]
  public Task<Customer> GetCustomers_TaskWithoutAsyncAwait()
  {
    return _taskService.Get(1, CustomersTake, CancellationToken.None);
  }

  [Benchmark]
  public async ValueTask<Customer> GetCustomers_ValueTask()
  {
    return await _valueTaskService.Get(1, CustomersTake, CancellationToken.None);
  }
  [Benchmark]
  public ValueTask<Customer> GetCustomers_ValueTaskWithoutAsyncAwait()
  {
    return _valueTaskService.Get(1, CustomersTake, CancellationToken.None);
  }

  // [Benchmark(Baseline = true)]
  // public async Task<long> GetCustomers_Task()
  // {
  //   counter = GetCounter(counter);
  //   long total = 0;
  //   for (int i = 0; i < Iterations; i++)
  //   {
  //     var customers = await _taskService.Get(counter, CustomersTake, CancellationToken.None);
  //
  //     foreach (var customer in customers)
  //     {
  //       total += customer.Revenue;
  //     }
  //   }
  //   return total;
  // }
  //
  // [Benchmark]
  // public async ValueTask<long> GetCustomers_ValueTask()
  // {
  //   counter = GetCounter(counter);
  //   long total = 0;
  //   for (int i = 0; i < Iterations; i++)
  //   {
  //     var customers = await _valueTaskService.Get(counter, CustomersTake, CancellationToken.None);
  //
  //     foreach (var customer in customers)
  //     {
  //       total += customer.Revenue;
  //     }
  //   }
  //   return total;
  // }

  private class Config : ManualConfig
  {
    public Config()
    {
      SummaryStyle = SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend);
    }
  }

  private int GetCounter(int counter)
  {
    if (counter + CustomersTake >= CustomersCount)
    {
      return 1;
    }
    return ++counter;
  }
}

internal sealed class TaskService
{
  private readonly ConcurrentDictionary<int, Customer> _cache = new();
  private readonly TaskRepository _repo;
  public TaskService()
  {
    _repo = new TaskRepository();
  }
  internal async Task<Customer> Get(int firstId, int take, CancellationToken cancellationToken)
  {
    if (!_cache.ContainsKey(firstId))
    {
      var customer = await _repo.Get(firstId, take, cancellationToken);
      _cache.TryAdd(firstId, customer);
    }
    return _cache[firstId];
    // return await _repo.Get(firstId, take, cancellationToken);
  }
}

// internal sealed class TaskService1
// {
//   private readonly TaskService _repo;
//   public TaskService1()
//   {
//     _repo = new TaskService();
//   }
//   internal async Task<List<Customer>> Get(int firstId, int take, CancellationToken cancellationToken)
//   {
//     return await _repo.Get(firstId, take, cancellationToken);
//   }
// }
// internal sealed class TaskService2
// {
//   private readonly TaskService1 _repo;
//   public TaskService2()
//   {
//     _repo = new TaskService1();
//   }
//   internal async Task<List<Customer>> Get(int firstId, int take, CancellationToken cancellationToken)
//   {
//     return await _repo.Get(firstId, take, cancellationToken);
//   }
// }
//

internal sealed class TaskRepository
{
  private readonly TestContext _context;
  public TaskRepository()
  {
    _context = new TestContext();
  }
  internal async Task<Customer> Get(int firstId, int take, CancellationToken cancellationToken)
    {
      return new Customer(await _context.Customers.FirstAsync(cancellationToken));
      // return await _context.Customers
      //   .Where(x => x.Id >= firstId)
      //   .Take(take)
      //   .Select(x => new Customer(x))
      //   .ToListAsync(cancellationToken);
    }
}

internal sealed class ValueTaskService
{
  private readonly ConcurrentDictionary<int, Customer> _cache = new ();
  private readonly ValueTaskRepository _repo;
  public ValueTaskService()
  {
    _repo = new ValueTaskRepository();
  }
  internal async ValueTask<Customer> Get(int firstId, int take, CancellationToken cancellationToken)
  {
    if (!_cache.ContainsKey(firstId))
    {
      var weather = await _repo.Get(firstId, take, cancellationToken);
      _cache.TryAdd(firstId, weather);
    }
    return _cache[firstId];
  }
}
// internal sealed class ValueTaskService1
// {
//   private readonly ValueTaskService _repo;
//   public ValueTaskService1()
//   {
//     _repo = new ValueTaskService();
//   }
//   internal async ValueTask<List<Customer>> Get(int firstId, int take, CancellationToken cancellationToken)
//   {
//     return await _repo.Get(firstId, take, cancellationToken);
//   }
// }
//
// internal sealed class ValueTaskService2
// {
//   private readonly ValueTaskService1 _repo;
//   public ValueTaskService2()
//   {
//     _repo = new ValueTaskService1();
//   }
//   internal async ValueTask<List<Customer>> Get(int firstId, int take, CancellationToken cancellationToken)
//   {
//     return await _repo.Get(firstId, take, cancellationToken);
//   }
// }

internal sealed class ValueTaskRepository
{
  private readonly TestContext _context;
  public ValueTaskRepository()
  {
    _context = new TestContext();
  }
  internal async ValueTask<Customer> Get(int firstId, int take, CancellationToken cancellationToken)
  {
    return new Customer(await _context.Customers.FirstAsync(cancellationToken));
      // .Where(x => x.Id >= firstId)
      // .Take(take)
      // .Select(x => new Customer(x))
      // .ToListAsync(cancellationToken);
  }
}

public class Customer
{
  public int Id { get; set; }
  public string Name { get; set; }
  public long Revenue { get; set; }

  public Customer(CustomerEntity customerEntity)
  {
    Id = customerEntity.Id;
    Name = customerEntity.Name;
    Revenue = customerEntity.Revenue;
  }
}