using System;
using Machine.Specifications;
using Machine.Specifications.Runner;
using Microsoft.Dnx.Testing.Abstractions;

namespace DerAlbert.Runner.MSpec.VisualStudio
{
    public class VisualStudioSpecificationRunListener : ISpecificationRunListener
    {
        private readonly ITestExecutionSink sink;
        private ContextInfo currentContext;
        public VisualStudioSpecificationRunListener(IServiceProvider services)
        {
            sink = (ITestExecutionSink)services.GetService(typeof(ITestExecutionSink));
        }

        public void OnAssemblyStart(AssemblyInfo assembly)
        {
        }

        public void OnAssemblyEnd(AssemblyInfo assembly)
        {
        }

        public void OnRunStart()
        {
        }

        public void OnRunEnd()
        {
        }

        public void OnContextStart(ContextInfo context)
        {
            currentContext = context;
        }

        public void OnContextEnd(ContextInfo context)
        {
            currentContext = null;
        }

        public void OnSpecificationStart(SpecificationInfo specification)
        {
            if (currentContext != null)
            {
                sink?.RecordStart(currentContext.GetVisualStudioTest(specification));
            }
        }

        public void OnSpecificationEnd(SpecificationInfo specification, Result result)
        {
            if (currentContext != null)
            {
                sink?.RecordResult(currentContext.GetVisualStudioTestResult(specification, result));
            }
        }

        public void OnFatalError(ExceptionResult exception)
        {
        }
    }
}