using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
BenchmarkRunner.Run<CollectionIndexerLastBenchmark>( /*new DebugInProcessConfig()*/);

[MemoryDiagnoser]
[DisassemblyDiagnoser]
[RankColumn]
[Config(typeof(Config))]
// [SimpleJob(RuntimeMoniker.Net60, baseline:true)]
// [SimpleJob(RuntimeMoniker.Net70)]
[SimpleJob(RuntimeMoniker.Net80)]
public class CollectionIndexerLastBenchmark
{

  private readonly IList<string> _collection = Regex.Matches(
      new HttpClient().GetStringAsync("https://www.gutenberg.org/cache/epub/100/pg100.txt").Result, @"\b\w+\b")
    .Cast<Match>()
    .GroupBy(word => word.Value, StringComparer.OrdinalIgnoreCase)
    .Select(x => x.Key)
    .ToList();
  
  [Benchmark(Baseline = true)]
  public string Last()
  {
    
    return _collection.Last();
    
  }

  [Benchmark]
  public string Indexer()
  {
    
    return _collection[_collection.Count - 1];
    
  }
  
  [Benchmark]
  public string ElementAt()
  {
    return _collection.ElementAt(^1);
  }
  
  private class Config : ManualConfig
  {
    public Config()
    {
      SummaryStyle = SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend);
    }
  }
}
