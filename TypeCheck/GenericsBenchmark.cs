using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.DependencyInjection;
// BenchmarkRunner.Run<GenericsBenchmark>( /*new DebugInProcessConfig()*/);

[MemoryDiagnoser]
[RankColumn]
[Config(typeof(Config))]
// [SimpleJob(RuntimeMoniker.Net60, baseline:true)]
// [SimpleJob(RuntimeMoniker.Net70)]
[SimpleJob(RuntimeMoniker.Net80)]

public class GenericsBenchmark
{
  private GenericType<string> _sourceReferenceType = new GenericType<string>("a");
  private GenericType<int> _sourceValueType = new GenericType<int>(1);
  
  [Benchmark(Baseline = true)]
  public bool ReferenceTypeWithIs() => _sourceReferenceType.Data is string;
  
  [Benchmark]
  public bool ReferenceTypeWithTypeCheck() => _sourceReferenceType.Data.GetType() == typeof(string);

  [Benchmark] 
  public bool ValueTypeWithIs() => _sourceValueType.Data is int;
  
  [Benchmark] 
  public bool ValueTypeWithTypeCheck() => _sourceValueType.Data.GetType() == typeof(int);
  
  private class Config : ManualConfig
  {
    public Config()
    {
      SummaryStyle = SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend);
    }
  }
}

internal sealed class GenericType<T>// where T : class
{
  public GenericType(T data)
  {
    Data = data;
  }
  internal T Data { get; } = default;
}