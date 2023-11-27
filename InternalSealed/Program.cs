using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.DependencyInjection;
// BenchmarkRunner.Run<Benchmark>( /*new DebugInProcessConfig()*/);

[MemoryDiagnoser]
[RankColumn]
[Config(typeof(Config))]
// [SimpleJob(RuntimeMoniker.Net60, baseline:true)]
// [SimpleJob(RuntimeMoniker.Net70)]
[SimpleJob(RuntimeMoniker.Net80)]
public class Benchmark
{
  // [Params(100 , 10000)]
  public int Iterations { get; set; } = 100;

  private IServiceProvider _serviceProvider;

  [GlobalSetup]
  public void Setup()
  {
    var services = new ServiceCollection();
    services
      .AddTransient<IPublicClass, PublicClass>()
      .AddTransient<IPublicSealedClass, PublicSealedClass>()
      .AddTransient<IInternalClass, InternalClass>()
      .AddTransient<IInternalSealedClass, InternalSealedClass>();
    var provider = services.BuildServiceProvider();
    _serviceProvider = provider;
  }

  [Benchmark(Baseline = true)]
  public int PublicClass()
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
  
  [Benchmark]
  public int PublicSealedClass()
  {
    int total = 0;
    for (int i = 0; i < Iterations; i++)
    {
      total += _serviceProvider
        .GetRequiredService<IPublicSealedClass>()
        .GetInt();
    }
    return total;
  }
  
  [Benchmark]
  public int InternalClass()
  {
    int total = 0;
    for (int i = 0; i < Iterations; i++)
    {
      total += _serviceProvider
        .GetRequiredService<IInternalClass>()
        .GetInt();
    }
    return total;
  }
  
  [Benchmark]
  public int InternalSealedClass()
  {
    int total = 0;
    for (int i = 0; i < Iterations; i++)
    {
      total += _serviceProvider
        .GetRequiredService<IInternalSealedClass>()
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

internal interface IPublicClass
{
  int GetInt();
}

internal interface IInternalClass
{
  int GetInt();
}

internal interface IInternalSealedClass
{
  int GetInt();
}

internal interface IPublicSealedClass
{
  int GetInt();
}

public class PublicClass : IPublicClass
{
  public int GetInt()
  {
    return 42;
  }
}

internal class InternalClass : IInternalClass
{
  public int GetInt()
  {
    return 42;
  }
}

internal sealed class InternalSealedClass : IInternalSealedClass
{
  public int GetInt()
  {
    return 42;
  }
}

internal sealed class PublicSealedClass : IPublicSealedClass
{
  public int GetInt()
  {
    return 42;
  }
}
