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
  
  [Params(/*10, 25,*/ 100)]
  public int CustomersTake { get; set; }

  private static int counter = 1;

  private IMediator _mediator;
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
    _mediator = provider.GetRequiredService<IMediator>();

    SampleData.Initialize(provider, CustomersCount);
  }

  [Benchmark(Baseline = true)]
  public async Task GetCustomers()
  {
    counter = GetCounter(counter);
    var customers = await _mediator.Send(new GetCustomersQuery(counter, CustomersTake), CancellationToken.None);
    var x = customers.Sum(x => x.Revenue);
    x += x;
  }

  [Benchmark]
  public async Task GetCustomers_WithAsNoTracking()
  {
    counter = GetCounter(counter);
    var customers = await _mediator.Send(new GetCustomersWithAsNoTrackingQuery(counter, CustomersTake), CancellationToken.None);
    var x = customers.Sum(x => x.Revenue);
    x += x;
  }

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


public class GetCustomersQuery : IRequest<List<Customer>>
{
  public int FirstId { get; set; }
  public int Take { get; }
  public GetCustomersQuery(int firstId, int take)
  {
    FirstId = firstId;
    Take = take;
  }
}

public class GetCustomersWithAsNoTrackingQuery : IRequest<List<Customer>>
{
  public int FirstId { get; set; }
  public int Take { get; }
  public GetCustomersWithAsNoTrackingQuery(int firstId, int take)
  {
    FirstId = firstId;
    Take = take;
  }
}

public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, List<Customer>>
{
  private readonly TestContext _context;
  public GetCustomersQueryHandler( /*TestContext context*/)
  {
    _context = new TestContext();
  }

  public Task<List<Customer>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
  {
    return _context.Customers
      .Where(x => x.Id >= request.FirstId)
      .Take(request.Take)
      .Select(x => new Customer(x))
      .ToListAsync(cancellationToken);
  }
}

public class GetCustomersWithAsNoTrackingQueryHandler : IRequestHandler<GetCustomersWithAsNoTrackingQuery, List<Customer>>
{
  private readonly TestContext _context;
  public GetCustomersWithAsNoTrackingQueryHandler( /*TestContext context*/)
  {
    _context = new TestContext();
  }
  public Task<List<Customer>> Handle(GetCustomersWithAsNoTrackingQuery request, CancellationToken cancellationToken)
  {
    return _context.Customers
      .Where(x => x.Id >= request.FirstId)
      .Take(request.Take)
      .AsNoTracking()
      .Select(x => new Customer(x))
      .ToListAsync(cancellationToken);
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