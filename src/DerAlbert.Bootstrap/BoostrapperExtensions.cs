using Microsoft.Extensions.DependencyInjection;
using StructureMap;

namespace DerAlbert.Bootstrap
{
    public static class BoostrapperExtensions
    {
        public static void UseBootstrapper(this IServiceCollection services, params string[] assemblyNames)
        {
            var container = new Container();
            var bootstrapper = new Bootstrapper(container);
            bootstrapper.ScanAssemblies(assemblyNames);
            services.AddInstance(bootstrapper);
            container.Populate(services);
        }
    }
}