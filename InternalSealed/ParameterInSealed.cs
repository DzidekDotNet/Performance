using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.DependencyInjection;
BenchmarkRunner.Run<ParameterInSealedBenchmark>( /*new DebugInProcessConfig()*/);

[MemoryDiagnoser]
[RankColumn]
[Config(typeof(Config))]
// [SimpleJob(RuntimeMoniker.Net60, baseline:true)]
// [SimpleJob(RuntimeMoniker.Net70)]
[SimpleJob(RuntimeMoniker.Net80)]

public class ParameterInSealedBenchmark
{
  private SealedType _sealed = new();
  private NonSealedType _nonSealed = new();

  [Benchmark/*(Baseline = true)*/]
  public int ParameterInNonSealed() => _nonSealed.M() + 42;

  [Benchmark]
  public int ParameterInSealed() => _sealed.M() + 42;

  public class BaseType
  {
    public virtual int M() => 1;
  }

  public class NonSealedType : BaseType
  {
    public override int M() => 2;
  }

  public sealed class SealedType : BaseType
  {
    public override int M() => 2;
  }
  
  private class Config : ManualConfig
  {
    public Config()
    {
      SummaryStyle = SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend);
    }
  }
}
