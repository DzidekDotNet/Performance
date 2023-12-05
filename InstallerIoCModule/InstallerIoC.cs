using InstallerIoCModule.Feature1;
using InstallerIoCModule.Feature2;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InstallerIoCModule;

public static class InstallerIoC
{
  public static IServiceCollection AddModule(this IServiceCollection services, IConfiguration configuration) =>
    services
      .AddFeature1(configuration)
      .AddFeature2(configuration);
}
