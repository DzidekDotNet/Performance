using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using DTO;

// BenchmarkRunner.Run<BenchmarkObjectResult>();

[MemoryDiagnoser]
[RankColumn]
[Config(typeof(Config))]
// [SimpleJob(RuntimeMoniker.Net60, baseline:true)]
// [SimpleJob(RuntimeMoniker.Net70)]
// [SimpleJob(RuntimeMoniker.Net80)]
public class BenchmarkObjectResult
{
  // [Params(100 , 10000)]
  public int Iterations { get; set; } = 100;
  
  private ObjectResultClassService _classService;
  private ObjectResultRecordService _recordService;
  private ObjectResultStructService _structService;
  private ObjectResultRecordStructService _recordStructService;
  private ObjectResultTupleService _tupleService;
  [GlobalSetup]
  public void Setup()
  {
    var repo = new Repository();
    _classService = new ObjectResultClassService(repo);
    _recordService = new ObjectResultRecordService(repo);
    _structService = new ObjectResultStructService(repo);
    _recordStructService = new ObjectResultRecordStructService(repo);
    _tupleService = new ObjectResultTupleService(repo);
  }
  [Benchmark(Baseline = true)]
  public long ClassDTO()
  {
    long total = 0;
    for (int i = 0; i < Iterations; i++)
    {
      var customers = _classService.Get();
      foreach (var customer in customers.Data)
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
      foreach (var customer in customers.Data)
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
      foreach (var customer in customers.Data)
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
      foreach (var customer in customers.Data)
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
      foreach (var customer in customers.Data)
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


internal class ObjectResult<T>
{
  internal T Data { get; }
  public ObjectResult(T data)
  {
    Data = data;
  }
}

internal sealed class ObjectResultClassService
{
  private readonly Repository _repository;
  public ObjectResultClassService(Repository repository)
  {
    _repository = repository;
  }
  internal ObjectResult<IEnumerable<CustomerClassDTO>> Get()
  {
    return new ObjectResult<IEnumerable<CustomerClassDTO>>(_repository.GetCustomers().Select(x => new CustomerClassDTO(x)));
  }
}

internal sealed class ObjectResultRecordService
{
  private readonly Repository _repository;
  public ObjectResultRecordService(Repository repository)
  {
    _repository = repository;
  }
  internal ObjectResult<IEnumerable<CustomerRecordDTO>> Get()
  {
    return new ObjectResult<IEnumerable<CustomerRecordDTO>>(_repository.GetCustomers().Select(x => new CustomerRecordDTO(x)));
  }
}

internal sealed class ObjectResultStructService
{
  private readonly Repository _repository;
  public ObjectResultStructService(Repository repository)
  {
    _repository = repository;
  }
  internal ObjectResult<IEnumerable<CustomerStructDTO>> Get()
  {
    return new ObjectResult<IEnumerable<CustomerStructDTO>>(_repository.GetCustomers().Select(x => new CustomerStructDTO(x)));
  }
}

internal sealed class ObjectResultRecordStructService
{
  private readonly Repository _repository;
  public ObjectResultRecordStructService(Repository repository)
  {
    _repository = repository;
  }
  internal ObjectResult<IEnumerable<CustomerRecordStructDTO>> Get()
  {
    return new ObjectResult<IEnumerable<CustomerRecordStructDTO>>(_repository.GetCustomers().Select(x => new CustomerRecordStructDTO(x)));
  }
}

internal sealed class ObjectResultTupleService
{
  private readonly Repository _repository;
  public ObjectResultTupleService(Repository repository)
  {
    _repository = repository;
  }
  internal ObjectResult<IEnumerable<(int Id, string Name, long Revenue)>> Get()
  {
    return new ObjectResult<IEnumerable<(int Id, string Name, long Revenue)>>(_repository.GetCustomers().Select(x => (x.Id, x.Name, x.Revenue)));
  }
}

