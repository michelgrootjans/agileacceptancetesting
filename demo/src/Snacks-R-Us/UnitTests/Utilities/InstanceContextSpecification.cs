using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using Snacks_R_Us.Domain.IoC;
using Snacks_R_Us.Domain.Mapping;

namespace Snacks_R_Us.UnitTests.Utilities
{
    [TestFixture]
    public abstract class InstanceContextSpecification<SUT>
    {
        protected SUT sut;
        private List<object> dependencies;
        private IMapperFactory mapperFactory;

        [SetUp]
        public void SetUp()
        {
            dependencies = new List<object>();
            Container.InitializeWith(new DictionaryContainer(dependencies));
            mapperFactory = RegisterDependencyInContainer<IMapperFactory>();

            Arrange();
            sut = CreateSystemUnderTest();
            Act();
        }

        protected virtual void Arrange()
        {
        }

        protected abstract SUT CreateSystemUnderTest();

        protected virtual void Act()
        {
        }

        protected T Dependency<T>() where T : class
        {
            return MockRepository.GenerateStub<T>();
        }

        protected T RegisterDependencyInContainer<T>() where T : class
        {
            var dependency = MockRepository.GenerateStub<T>();
            dependencies.Add(dependency);
            return dependency;
        }

        protected IStubSpecification<Target> When<Target>(Target target) where Target : class
        {
            return new StubSpecification<Target>(target);
        }

        protected IMapper<From, To> RegisterMapper<From, To>()
        {
            var mapper = Dependency<IMapper<From, To>>();
            When(mapperFactory).IsToldTo(f => f.GetMapper<From, To>()).Return(mapper);
            return mapper;
        }
    }
}