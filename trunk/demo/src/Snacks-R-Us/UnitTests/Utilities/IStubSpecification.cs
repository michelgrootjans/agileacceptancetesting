using Rhino.Mocks;
using Rhino.Mocks.Interfaces;

namespace Snacks_R_Us.UnitTests.Utilities
{
    public interface IStubSpecification<Target>
    {
        IMethodOptions<Result> IsToldTo<Result>(Function<Target, Result> function);
    }

    public class StubSpecification<Target> : IStubSpecification<Target> where Target : class
    {
        private readonly Target target;

        public StubSpecification(Target target)
        {
            this.target = target;
        }

        public IMethodOptions<Result> IsToldTo<Result>(Function<Target, Result> function)
        {
            return target.Stub(function);
        }
    }

}