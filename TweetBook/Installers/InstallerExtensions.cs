using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TweetBook.Installers
{
    public static class InstallerExtensions
    {
        public static void InstallServicesInAssembly(IServiceCollection services, IConfiguration configuration)
        {
            var installers = typeof(Startup)
                .Assembly
                .GetExportedTypes()
                .Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsAbstract && !x.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<IInstaller>()
                .ToList();

            installers.ForEach(x => x.InstallServices(services, configuration));
        }
    }
}
