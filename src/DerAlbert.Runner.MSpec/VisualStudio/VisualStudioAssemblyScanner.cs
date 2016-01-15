using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Machine.Specifications.Explorers;
using Machine.Specifications.Model;
using Microsoft.Dnx.Testing.Abstractions;

namespace DerAlbert.Runner.MSpec.VisualStudio
{
    public class VisualStudioAssemblyScanner
    {
        private readonly IServiceProvider services;
        private readonly ISourceInformationProvider provider;
        public VisualStudioAssemblyScanner(IServiceProvider services)
        {
            this.services = services;
            provider = (ISourceInformationProvider)services.GetService(typeof(ISourceInformationProvider));
        }

        public void SendToVisualStudio(Assembly assembly)
        {
            var explorer = new AssemblyExplorer();
            var sink = (ITestDiscoverySink)services.GetService(typeof(ITestDiscoverySink));
            var contexts = explorer.FindContextsIn(assembly);
            foreach (var context in contexts)
            {
                foreach (var test in ConvertToTests(context))
                {
                    sink?.SendTest(test);
                }
            }
        }

        private IEnumerable<Test> ConvertToTests(Context context)
        {
            return context.Specifications.Select(specification => ConvertToTest(context,specification));
        }

        public Test ConvertToTest(Context context, Specification specification)
        {
            var test = context.GetVisualStudioTest(specification);
            var it  = (Delegate)specification.FieldInfo.GetValue(context.Instance);
            var methodInfo = it.GetMethodInfo();
            var sourceInfo = provider.GetSourceInformation(methodInfo);
            test.CodeFilePath = sourceInfo.Filename;
            test.LineNumber = sourceInfo.LineNumber;
            return test;
        }
    }
}