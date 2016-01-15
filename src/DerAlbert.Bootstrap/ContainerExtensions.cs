using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.PlatformAbstractions;
using StructureMap.Graph;

namespace DerAlbert.Bootstrap
{
    public static class ContainerExtensions
    {

        public static void AssembliesFromLibraryManager(this IAssemblyScanner scanner, Func<Assembly, bool> filter)
        {
            var loadContext = PlatformServices.Default.AssemblyLoadContextAccessor.Default;
            var libraryManager = PlatformServices.Default.LibraryManager;
            var assemblyNames = libraryManager.GetLibraries().SelectMany(l => l.Assemblies);
            var scanned = new List<string>();
            foreach (var assemblyName in assemblyNames)
            {
                if (scanned.Contains(assemblyName.FullName))
                {
                    continue;
                }
                var assembly = loadContext.Load(assemblyName);
                scanned.Add(assemblyName.FullName);
                if (filter(assembly))
                {
                    scanner.Assembly(assembly);
                }
            }
        }
    }
}