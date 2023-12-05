using InstallerIoCModule.Feature1.SubFeature1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InstallerIoCModule.Feature1;

internal static class InstallerIoC
{
  internal static IServiceCollection AddFeature1(this IServiceCollection services, IConfiguration configuration) => 
    services
      .AddSubFeature1(configuration);
}
