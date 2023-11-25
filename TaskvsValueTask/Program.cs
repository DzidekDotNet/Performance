using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Reports;
using EntityFrameworkWithMediatR;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
// BenchmarkRunner.Run<Benchmark>(/*new DebugInProcessConfig()*/);

[MemoryDiagnoser]
[RankColumn]
[Config(typeof(Config))]
public class Benchmark
{
  [Params(1000 , 10000, 250000)]
  public int CustomersCount { get; set; }

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
  public async Task GetCustomersRevenue()
  {
    await _mediator.Send(new GetCustomersRevenueQuery(), CancellationToken.None);
  }

  [Benchmark]
  public async Task GetCustomersRevenue_WithAsNoTracking()
  {
    await _mediator.Send(new GetCustomersRevenueWithAsNoTrackingQuery(), CancellationToken.None);
  }

  private class Config : ManualConfig
  {
    public Config()
    {
      SummaryStyle = SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend);
    }
  }
}


public class GetCustomersRevenueQuery : IRequest<long>
{
}

public class GetCustomersRevenueWithAsNoTrackingQuery : IRequest<long>
{
}

public class GetCustomersRevenueQueryHandler : IRequestHandler<GetCustomersRevenueQuery, long>
{
  private readonly TestContext _context;
  public GetCustomersRevenueQueryHandler( /*TestContext context*/)
  {
    _context = new TestContext();
  }
  // public Task<long> Handle(GetCustomersRevenueQuery request, CancellationToken cancellationToken) =>
  //   _context.Customers.SumAsync(x => x.Revenue, cancellationToken);  
  public Task<long> Handle(GetCustomersRevenueQuery request, CancellationToken cancellationToken)
  {
    long sum = 0;
    foreach (var customer in _context.Customers)
    {
      sum += customer.Revenue;
    }
    return Task.FromResult(sum);
  }
}

public class GetCustomersRevenueWithAsNoTrackingQueryHandler : IRequestHandler<GetCustomersRevenueWithAsNoTrackingQuery, long>
{
  private readonly TestContext _context;
  public GetCustomersRevenueWithAsNoTrackingQueryHandler( /*TestContext context*/)
  {
    _context = new TestContext();
  }
  public Task<long> Handle(GetCustomersRevenueWithAsNoTrackingQuery request, CancellationToken cancellationToken)
  {
    long sum = 0;
    foreach (var customer in _context.Customers.AsNoTracking())
    {
      sum += customer.Revenue;
    }
    return Task.FromResult(sum);
  }
}
