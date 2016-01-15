using System;
using System.Linq;
using Machine.Specifications.Runner;
using Machine.Specifications.Runner.Impl;
using Microsoft.Extensions.PlatformAbstractions;

namespace DerAlbert.Runner.MSpec
{
    public class Program
    {
        private IApplicationEnvironment appEnv;
        private ILibraryManager libraryManager;
        private IAssemblyLoadContext loadContext;

        public Program()
        {
            appEnv = PlatformServices.Default.Application;
            libraryManager = PlatformServices.Default.LibraryManager;
            loadContext = PlatformServices.Default.AssemblyLoadContextAccessor.Default;
        }

        public void Main(string[] args)
        {
            Run(args);
        }

        private void Run(string[] args)
        {
            var library = libraryManager.GetLibrary(appEnv.ApplicationName);
            var assembly = loadContext.Load(library.Assemblies.First());
            var runner = new DefaultRunner(new DnxListener(new ColorOutput(new VerboseOutput(new DefaultConsole()))),RunOptions.Default);
            runner.RunAssembly(assembly);
        }
    }
}