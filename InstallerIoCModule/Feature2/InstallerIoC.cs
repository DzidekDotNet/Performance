using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InstallerIoCModule.Feature2;

internal static class InstallerIoC
{
  internal static IServiceCollection AddFeature2(this IServiceCollection services, IConfiguration configuration) => 
    services;
}
