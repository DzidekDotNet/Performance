using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InstallerIoCModule.Feature1.SubFeature1;

internal static class InstallerIoC
{
  internal static IServiceCollection AddSubFeature1(this IServiceCollection services, IConfiguration configuration) => 
    services;
}
