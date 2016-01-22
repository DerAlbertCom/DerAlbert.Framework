using System;
using System.Collections.Generic;
using System.Linq;
using StructureMap;

namespace DerAlbert.Bootstrap
{
    [UsedImplicitly]
    public class Bootstrapper
    {
        private readonly Container container;

        public Bootstrapper(Container container)
        {
            this.container = container;
        }

        public void Execute()
        {
            var instances = GetAllBootstrapItems().OrderBy(b => b.Order);
            foreach (var bootstrapItem in instances)
            {
                bootstrapItem.Execute();
            }
        }

        private void DisposeAll()
        {
            var instances = GetAllBootstrapItems().OrderByDescending(b => b.Order);
            foreach (var bootstrapItem in instances)
            {
                bootstrapItem.Dispose();
            }
        }

        private IEnumerable<IBootstrapItem> GetAllBootstrapItems()
        {
            return container.GetAllInstances<IBootstrapItem>();
        }

        public void ScanAssemblies(IEnumerable<string> assemblyNames)
        {
            container.Configure(c =>
            {
                c.Scan(s =>
                {
                    s.AssembliesFromLibraryManager(assembly =>
                    { return assemblyNames.Any(assemblyName => assembly.FullName.StartsWith(assemblyName)); });
                    s.AddAllTypesOf<IBootstrapItem>();
                    s.WithDefaultConventions();
                    s.LookForRegistries();
                });

                c.For(typeof(Lazy<>))
                    .Use(typeof(Lazy<>));
            });
        }


    }
}