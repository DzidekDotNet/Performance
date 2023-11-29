using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.DependencyInjection;
BenchmarkRunner.Run<CollectionsBenchmark>( /*new DebugInProcessConfig()*/);

[MemoryDiagnoser]
[RankColumn]
[Config(typeof(Config))]
// [SimpleJob(RuntimeMoniker.Net60, baseline:true)]
// [SimpleJob(RuntimeMoniker.Net70)]
[SimpleJob(RuntimeMoniker.Net80)]

public class CollectionsBenchmark
{
  private IEnumerable<int> _source = (int[])(object)new uint[42];
  
  [Benchmark(Baseline = true)]
  public bool WithIsAsArray() => _source is int[];
  
  [Benchmark]
  public bool WithTypeCheckAsArray() => _source.GetType() == typeof(int[]);
  
  [Benchmark]
  public bool WithIs() => _source is IEnumerable<int>;

  [Benchmark]
  public bool WithTypeCheck() => _source.GetType() == typeof(IEnumerable<int>);
  
  private class Config : ManualConfig
  {
    public Config()
    {
      SummaryStyle = SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend);
    }
  }
}
