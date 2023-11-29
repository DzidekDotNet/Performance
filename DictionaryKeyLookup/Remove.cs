using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
// ReSharper disable CanSimplifyDictionaryLookupWithTryGetValue
// BenchmarkRunner.Run<RemoveBenchmark>( /*new DebugInProcessConfig()*/);

[MemoryDiagnoser]
[RankColumn]
[Config(typeof(Config))]
// [SimpleJob(RuntimeMoniker.Net60, baseline:true)]
// [SimpleJob(RuntimeMoniker.Net70)]
// [SimpleJob(RuntimeMoniker.Net80)]
public class RemoveBenchmark
{

  private readonly Dictionary<string, int> _counts = Regex.Matches(
      new HttpClient().GetStringAsync("https://www.gutenberg.org/cache/epub/100/pg100.txt").Result, @"\b\w+\b")
    .Cast<Match>()
    .GroupBy(word => word.Value, StringComparer.OrdinalIgnoreCase)
    .ToDictionary(word => word.Key, word => word.Count(), StringComparer.OrdinalIgnoreCase);

  private string _word = "the";
  
  [Benchmark(Baseline = true)]
  public void LookupWithContainsKey()
  {
    if (_counts.ContainsKey(_word))
    {
      _counts.Remove(_word);
    }
  }

  [Benchmark]
  public void LookupWithTryGetValue()
  {
    _counts.Remove(_word);
  }
  
  private class Config : ManualConfig
  {
    public Config()
    {
      SummaryStyle = SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend);
    }
  }
}