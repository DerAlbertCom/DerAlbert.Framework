using System;
using Machine.Specifications;
using Machine.Specifications.Model;
using Machine.Specifications.Runner;
using Machine.Specifications.Runner.Impl;
using Microsoft.Dnx.Testing.Abstractions;

namespace DerAlbert.Runner.MSpec.VisualStudio
{
    public static class VisualStudioConverterExtensions
    {
        public static Test GetVisualStudioTest(this Context context, Specification specification)
        {
            return context.GetInfo().GetVisualStudioTest(specification.GetInfo());
        }
        public static Test GetVisualStudioTest(this ContextInfo context, SpecificationInfo specification)
        {
            var test = new Test();
            test.Properties["Category"] = context.Concern;
            test.FullyQualifiedName = $"{context.FullName}:{specification.Leader}:{specification.Name}";
            test.DisplayName = $"{context.Name}:{specification.Leader}:{specification.Name}";
            return test;
        }

        public static TestResult GetVisualStudioTestResult(this ContextInfo context, SpecificationInfo specification, Result result)
        {
            var vsResult = new TestResult(context.GetVisualStudioTest(specification));
            switch (result.Status)
            {
                case Status.Failing:
                    vsResult.Outcome=TestOutcome.Failed;
                    break;
                case Status.Passing:
                    vsResult.Outcome=TestOutcome.Passed;
                    break;
                case Status.NotImplemented:
                    vsResult.Outcome = TestOutcome.NotFound;
                    break;
                case Status.Ignored:
                    vsResult.Outcome=TestOutcome.Skipped;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return vsResult;
        }
    }
}