using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
BenchmarkRunner.Run<CollectionIndexerBenchmark>( /*new DebugInProcessConfig()*/);

[MemoryDiagnoser]
[RankColumn]
[Config(typeof(Config))]
// [SimpleJob(RuntimeMoniker.Net60, baseline:true)]
// [SimpleJob(RuntimeMoniker.Net70)]
[SimpleJob(RuntimeMoniker.Net80)]
public class CollectionIndexerBenchmark
{

  private readonly IList<string> _collection = Regex.Matches(
      new HttpClient().GetStringAsync("https://www.gutenberg.org/cache/epub/100/pg100.txt").Result, @"\b\w+\b")
    .Cast<Match>()
    .GroupBy(word => word.Value, StringComparer.OrdinalIgnoreCase)
    .Select(x => x.Key)
    .ToList();
  
  [Benchmark(Baseline = true)]
  public string First()
  {
    return _collection.First();
  }

  [Benchmark]
  public string Indexer()
  {
    return _collection[0];
  }
  
  [Benchmark]
  public string ElementAt()
  {
    return _collection.ElementAt(0);
  }
  
  private class Config : ManualConfig
  {
    public Config()
    {
      SummaryStyle = SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend);
    }
  }
}
