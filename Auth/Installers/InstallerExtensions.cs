using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Api
{
    public static class InstallerExtensions
    {
        public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            var installerClasses = typeof(Startup).Assembly.ExportedTypes.Where(
               x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(Activator.CreateInstance).Cast<IInstaller>().ToList();

            installerClasses.ForEach(x => x.InstallServices(services, configuration));
        }
    }
}
