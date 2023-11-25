using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using DTO;
// BenchmarkRunner.Run<Benchmark>();

[MemoryDiagnoser]
[RankColumn]
[Config(typeof(Config))]
public class Benchmark
{
  // [Params(100 , 10000)]
  public int Iterations { get; set; } = 100;
  
  private ClassService _classService;
  private RecordService _recordService;
  private StructService _structService;
  private RecordStructService _recordStructService;
  private TupleService _tupleService;
  [GlobalSetup]
  public void Setup()
  {
    var repo = new Repository();
    _classService = new ClassService(repo);
    _recordService = new RecordService(repo);
    _structService = new StructService(repo);
    _recordStructService = new RecordStructService(repo);
    _tupleService = new TupleService(repo);
  }
  [Benchmark(Baseline = true)]
  public long ClassDTO()
  {
    long total = 0;
    for (int i = 0; i < Iterations; i++)
    {
      var customers = _classService.Get();
      foreach (var customer in customers)
      {
        total += customer.Revenue;
      }
    }
    return total;
  }
  [Benchmark]
  public long RecordDTO()
  {
    long total = 0;
    for (int i = 0; i < Iterations; i++)
    {
      var customers = _recordService.Get();
      foreach (var customer in customers)
      {
        total += customer.Revenue;
      }
    }
    return total;
  }

  [Benchmark]
  public long StructDTO()
  {
    long total = 0;
    for (int i = 0; i < Iterations; i++)
    {
      var customers = _structService.Get();
      foreach (var customer in customers)
      {
        total += customer.Revenue;
      }
    }
    return total;
  }
  [Benchmark]
  public long RecordStructDTO()
  {
    long total = 0;
    for (int i = 0; i < Iterations; i++)
    {
      var customers = _recordStructService.Get();
      foreach (var customer in customers)
      {
        total += customer.Revenue;
      }
    }
    return total;
  }
  [Benchmark]
  public long TupleDTO()
  {
    long total = 0;
    for (int i = 0; i < Iterations; i++)
    {
      var customers = _tupleService.Get();
      foreach (var customer in customers)
      {
        total += customer.Revenue;
      }
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

internal sealed class ClassService
{
  private readonly Repository _repository;
  public ClassService(Repository repository)
  {
    _repository = repository;
  }
  internal IEnumerable<CustomerClassDTO> Get()
  {
    return _repository.GetCustomers().Select(x => new CustomerClassDTO(x));
  }
}

internal sealed class RecordService
{
  private readonly Repository _repository;
  public RecordService(Repository repository)
  {
    _repository = repository;
  }
  internal IEnumerable<CustomerRecordDTO> Get()
  {
    return _repository.GetCustomers().Select(x => new CustomerRecordDTO(x));
  }
}

internal sealed class StructService
{
  private readonly Repository _repository;
  public StructService(Repository repository)
  {
    _repository = repository;
  }
  internal IEnumerable<CustomerStructDTO> Get()
  {
    return _repository.GetCustomers().Select(x => new CustomerStructDTO(x));
  }
}

internal sealed class RecordStructService
{
  private readonly Repository _repository;
  public RecordStructService(Repository repository)
  {
    _repository = repository;
  }
  internal IEnumerable<CustomerRecordStructDTO> Get()
  {
    return _repository.GetCustomers().Select(x => new CustomerRecordStructDTO(x));
  }
}

internal sealed class TupleService
{
  private readonly Repository _repository;
  public TupleService(Repository repository)
  {
    _repository = repository;
  }
  internal IEnumerable<(int Id, string Name, long Revenue)> Get()
  {
    return _repository.GetCustomers().Select(x => (x.Id, x.Name, x.Revenue));
  }
}

