using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.DependencyInjection;
BenchmarkRunner.Run<IoCGetServiceVsGetRequiredService>( /*new DebugInProcessConfig()*/);

[MemoryDiagnoser]
[RankColumn]
[Config(typeof(Config))]
// [SimpleJob(RuntimeMoniker.Net60, baseline:true)]
// [SimpleJob(RuntimeMoniker.Net70)]
[SimpleJob(RuntimeMoniker.Net80)]
public class IoCGetServiceVsGetRequiredService
{
  // [Params(100 , 10000)]
  public int Iterations { get; set; } = 10;

  private IServiceProvider _serviceProvider;

  [GlobalSetup]
  public void Setup()
  {
    var services = new ServiceCollection();
    services
      .AddTransient<IPublicClass, PublicClass>()
      .AddTransient<InstancePublicClass>();
    var provider = services.BuildServiceProvider();
    _serviceProvider = provider;
  }

  [Benchmark/*(Baseline = true)*/]
  public int GetRequiredService()
  {
    int total = 0;
    for (int i = 0; i < Iterations; i++)
    {
      total += _serviceProvider
        .GetRequiredService<IPublicClass>()
        .GetInt();
    }
    return total;
  }
  [Benchmark/*(Baseline = true)*/]
  public int GetService()
  {
    int total = 0;
    for (int i = 0; i < Iterations; i++)
    {
      total += _serviceProvider
        .GetService<IPublicClass>()
        .GetInt();
    }
    return total;
  }
  
  [Benchmark/*(Baseline = true)*/]
  public int GetInstanceRequiredService()
  {
    int total = 0;
    for (int i = 0; i < Iterations; i++)
    {
      total += _serviceProvider
        .GetRequiredService<InstancePublicClass>()
        .GetInt();
    }
    return total;
  }
  [Benchmark(Baseline = true)]
  public int GetInstanceService()
  {
    int total = 0;
    for (int i = 0; i < Iterations; i++)
    {
      total += _serviceProvider
        .GetService<InstancePublicClass>()
        .GetInt();
    }
    return total;
  }
  

  private class Config : ManualConfig
  {
    public Config()
    {
      SummaryStyle = SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend);
    }
  }
}

public class InstancePublicClass
{
  public int GetInt()
  {
    return 42;
  }
}