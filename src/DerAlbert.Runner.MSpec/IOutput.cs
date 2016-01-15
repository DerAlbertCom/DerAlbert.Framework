using Machine.Specifications;
using Machine.Specifications.Runner;

namespace DerAlbert.Runner.MSpec
{
    public interface IOutput
    {
        void RunStart();
        void RunEnd();
        void AssemblyStart(AssemblyInfo assembly);
        void AssemblyEnd(AssemblyInfo assembly);
        void ContextStart(ContextInfo context);
        void ContextEnd(ContextInfo context);
        void SpecificationStart(SpecificationInfo specification);
        void Passing(SpecificationInfo specification);
        void NotImplemented(SpecificationInfo specification);
        void Ignored(SpecificationInfo specification);
        void Failed(SpecificationInfo specification, Result result);
        void FatalError(ExceptionResult exception);
    }
}